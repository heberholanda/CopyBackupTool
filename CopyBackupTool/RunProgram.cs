using ConspiracaoCopy;
using CopyBackupTool.Models;
using System;
using System.Collections.Generic;

namespace CopyBackupTool
{
    public class Run
    {
        public static void Main(string[] args)
        {
            var run = new Operations();
            foreach (var config in run.JsonFileConfigs)
            {
                if (config.Status)
                {
                    Console.WriteLine("\n[ {0} ] Starting...", config.Title);
                    run.CompressFolder(config.CompressFolder);
                    run.CopyAndPaste(config.CopyAndPaste);
                    //run.ZipFolder(config.Backup);
                    //run.CopyAllFiles(config);
                    Console.WriteLine("[ {0} ] Finished!\n", config.Title);
                } else
                {
                    Console.WriteLine("\n[ {0} ] Not enabled. Status: {1}", config.Title, config.Status);
                }
            }
            Console.WriteLine("\nMy work is done! \n;]");
            Console.ReadKey();
        }
    }
}
