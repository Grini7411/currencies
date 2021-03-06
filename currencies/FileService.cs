using System;
using System.Collections.Generic;
using System.IO;

namespace currencies
{
    static class FileService
    {
        public static void RemoveTextFromFile()
        {
            if (File.Exists("./files/log.txt"))
            {
                File.WriteAllText("./files/log.txt", string.Empty);
            }
        }

        public static void WriteToFile(List<Currency> currencies)
        {
            string path = "./files/log.txt";
            FileStream fs;
            if (!Directory.Exists("./files"))
            {
                Directory.CreateDirectory("./files");
            }
            if (!File.Exists(path))
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            }
            StreamWriter writer = new StreamWriter(fs);

            foreach (Currency currency in currencies)
            {
                writer.WriteLine($"Pair name - {currency.Name}");
                writer.WriteLine($"Value - {currency.Value}");
                writer.WriteLine($"Updated - {currency.Updated}");
            }
            writer.Close();
            fs.Close();
        }
        public static void ReadFromFile()
        {
            FileStream fs = new FileStream("./files/log.txt",
                                     FileMode.Open,
                                     FileAccess.Read);

            StreamReader reader = new StreamReader(fs);

            string str = reader.ReadToEnd();
            Console.WriteLine(str);

            reader.Close();
            fs.Close();
        }

    }
}
