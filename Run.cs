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
            _Configs = run.loadJson("C:\\ConspiracaoCopy\\ConfigurationFile.json");

            foreach (var config in _Configs)
            {
                Console.WriteLine("\n[ {0} ] Starting...", config.Title);
                run.zipFolder(config.Backup);
                run.copyAllFiles(config);
                Console.WriteLine("[ {0} ] Finished!\n", config.Title);
            }

            Console.WriteLine("My work is done!");
            Console.ReadKey();
        }
    }
}
