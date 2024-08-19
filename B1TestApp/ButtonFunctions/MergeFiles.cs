using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace B1TestApp.Functions
{
    public static class MergeFiles
    {
        public static async Task<int> MergeFilesAndRemoveLinesAsync(string searchTerm)
        {
            string combinedFilePath = "combined.txt";
            int removedLines = 0;

            try
            {
                using (StreamWriter writer = new StreamWriter(combinedFilePath))
                {
                    for (int fileIndex = 0; fileIndex < 100; fileIndex++)
                    {
                        string filePath = $"file{fileIndex + 1}.txt";

                        if (!File.Exists(filePath))
                        {
                            continue;
                        }

                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            string line;
                            while ((line = await reader.ReadLineAsync()) != null)
                            {
                                if (!line.Contains(searchTerm))
                                {
                                    await writer.WriteLineAsync(line);
                                }
                                else
                                {
                                    removedLines++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

            return removedLines;
        }


    }
}