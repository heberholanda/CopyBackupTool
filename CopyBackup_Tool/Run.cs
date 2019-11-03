using ConspiracaoCopy;
using System;
using System.Collections.Generic;

namespace CopyBackup_Tool
{
    class Run
    {
        static void Main(string[] args)
        {
            var run = new Operations();
            var _Configs = new List<FileModel>();
            var configFilePath = AppDomain.CurrentDomain.BaseDirectory + "ConfigurationFile.json";
            _Configs = run.loadJson(configFilePath);

            foreach (var config in _Configs)
            {
                if (config.Enable)
                {
                    Console.WriteLine("\n[ {0} ] Starting...", config.Title);
                    run.zipFolder(config.Backup);
                    run.copyAllFiles(config);
                    Console.WriteLine("[ {0} ] Finished!\n", config.Title);
                } else
                {
                    Console.WriteLine("\n[ {0} ] Not run. Status: {1}", config.Title, config.Enable);
                }

            }

            Console.WriteLine("My work is done!");
            Console.ReadKey();
        }
    }
}
