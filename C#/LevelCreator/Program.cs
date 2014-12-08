using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace LevelCreator
{
    class Program
    {
        static void Main()
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

            while (blockList.Any(a => a.X < 0)) foreach (Block block in blockList) block.X++;
            while (blockList.Any(a => a.Y < 0)) foreach (Block block in blockList) block.Y++;
            while (blockList.Any(a => a.Z < 0)) foreach (Block block in blockList) block.Z++;


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
