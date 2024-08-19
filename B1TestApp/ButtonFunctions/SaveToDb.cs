using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using B1TestApp.Data;
using B1TestApp.Data.Entity;
using B1TestApp.Utilities;

namespace B1TestApp.Functions
{
    public static class SaveToDb
    {
        public static async Task ImportToDatabaseAsync(IProgress<(double, string)>? progress = null)
        {
            string combinedFilePath = "combined.txt";
            var batchSize = 1000; 
            var lines = File.ReadLines(combinedFilePath).ToList(); 
            var bag = new ConcurrentBag<DataRecord>(); 
            var tasks = new List<Task>();
            var numberOfTasks = Environment.ProcessorCount;
            var synchronizationContext = SynchronizationContext.Current;

            var linesPerTask = (int)Math.Ceiling((double)lines.Count / numberOfTasks);
            for (int taskIndex = 0; taskIndex < numberOfTasks; taskIndex++)
            {
                var start = taskIndex * linesPerTask;
                var end = Math.Min(start + linesPerTask, lines.Count);

                var task = Task.Run(() =>
                {
                    for (int i = start; i < end; i++)
                    {
                        var line = lines[i];
                        var parts = line.Split("||");

                        if (DateTimeOffset.TryParse(parts[0], null, DateTimeStyles.AssumeUniversal, out var date))
                        {
                            var latinText = parts[1];
                            var cyrillicText = parts[2];
                            var intValue = int.Parse(parts[3]);
                            var doubleValue = double.Parse(parts[4].Replace(',', '.'), CultureInfo.InvariantCulture);
                                
                            var dataRecord = new DataRecord
                            {
                                Date = date,
                                LatinText = latinText,
                                CyrillicText = cyrillicText,
                                IntValue = intValue,
                                DoubleValue = doubleValue
                            };

                            bag.Add(dataRecord);
                        }
                        else
                        {
                            Console.WriteLine($"Failed to parse date: {parts[0]}");
                        }
                    }
                });

                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
            
            var records = bag.ToList();
            var numberOfThreads = Environment.ProcessorCount; 
            var tasksForSaving = new List<Task>();
            var semaphore = new SemaphoreSlim(numberOfThreads); // Ограничиваем количество одновременно работающих потоков
            var totalRecords = records.Count;
            var processedRecords = new AtomicLong(0); // Используем потокобезопасный счетчик

            // Разделение записей на блоки
            var batches = records
                .Select((record, index) => new { record, index })
                .GroupBy(x => x.index / batchSize)
                .Select(g => g.Select(x => x.record).ToList())
                .ToList();

            foreach (var batch in batches)
            {
                await semaphore.WaitAsync(); // Ожидание свободного потока

                tasksForSaving.Add(Task.Run(async () =>
                {
                    try
                    {
                        using (var dbContext = new AppDbContext())
                        {
                            // Вставка данных в базу данных
                            dbContext.DataRecords.AddRange(batch);
                            await dbContext.SaveChangesAsync();
                            dbContext.ChangeTracker.Clear();

                            // Обновление прогресса
                            long value = processedRecords.Value;
                            var processedCount = Interlocked.Add(ref value, batch.Count);
                            processedRecords.Value = value;
                            var percentage = (double)processedCount / totalRecords * 100;
                            synchronizationContext?.Post(_ => progress?.Report((percentage, $"Импортировано строк: {processedCount}/{totalRecords}")), null);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Логирование ошибки
                        Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
                    }
                    finally
                    {
                        semaphore.Release(); // Освобождение потока
                    }
                }));
            }

            await Task.WhenAll(tasksForSaving); // Ожидание завершения всех задач
            progress?.Report((100.0, $"Импортировано строк: {records.Count}/{records.Count}"));
        }
            
    }
       

        
        
    
}