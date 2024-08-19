using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace B1TestApp.Functions
{
    public static class GenerateFunction
    {
        public static async Task GenerateFilesAsync()
        {
            string charsLatin = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string charsCyrillic = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Random random = new Random();
            DateTimeOffset startDate = DateTimeOffset.UtcNow.AddYears(-5);
            
            for (int fileIndex = 0; fileIndex < 100; fileIndex++)
            {
                string filePath = $"file{fileIndex + 1}.txt";
                
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i < 100000; i++)
                    {
                        DateTimeOffset randomDate = startDate.AddDays(random.Next(0, 1825)).ToUniversalTime();;
                        string randomLatin = new string(Enumerable.Repeat(charsLatin, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        string randomCyrillic = new string(Enumerable.Repeat(charsCyrillic, 10).Select(s => s[random.Next(s.Length)]).ToArray());
                        int randomInt = random.Next(1, 50000000) * 2;
                        double randomDouble = random.NextDouble() * 19 + 1;

                        await writer.WriteLineAsync($"{randomDate:dd.MM.yyyy}||{randomLatin}||{randomCyrillic}||{randomInt}||{randomDouble:F8}||");
                    }
                }
            }
        }

    }
    
}