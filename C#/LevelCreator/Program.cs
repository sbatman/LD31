using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace LevelCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string processedLevelFile = "GameLevel.txt";

            Console.Title = "LD31 Level Creator";

            Console.Write("File Name: ");

            string levelFile = Console.ReadLine();
            List<Block> blockList;

            // ReSharper disable once AssignNullToNotNullAttribute
            using (StreamReader sReader = new StreamReader(levelFile))
            {
                String jsonData = sReader.ReadToEnd();
                
                blockList = JsonConvert.DeserializeObject<List<Block>>(jsonData);
            }

            Console.WriteLine();
            Console.WriteLine("Level Data Read From File... Re-Saving...");
            Console.WriteLine();

            FileStream fStream = new FileStream(processedLevelFile, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sWriter = new StreamWriter(fStream);

            foreach (Block b in blockList)
            {
                sWriter.WriteLine(b.ToString());
            }

            sWriter.Flush();
            fStream.Flush();

            sWriter.Dispose();
            fStream.Dispose();

            Console.WriteLine("Level Data Successfully Saved");
            
            Console.ReadKey();
        }
    }
}
