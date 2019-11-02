using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConspiracaoCopy
{
    public class Operations
    {
        public List<FileModel> loadJson(string cfgFile)
        {
            try
            {
                using (StreamReader r = new StreamReader(cfgFile))
                {
                    Console.WriteLine("Load Configs.");
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<FileModel>>(json);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Configuration file not found!");
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void zipFolder(ZipFolder backup)
        {
            if (backup.Enable != true) return;
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.UseUnicodeAsNecessary = true;  // utf-8
                    zip.AddDirectory(@backup.SourcePath);
                    zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
                    zip.Save(@backup.MoveToPath);
                }
            }
            catch (Ionic.Zip.ZipException e)
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void copyAllFiles(FileModel config)
        {
            if (String.IsNullOrEmpty(config.SourcePath)) {
                Console.WriteLine("[ {0} ] The SourcePath is emply.", config.Title);
                return;
            }
            if (String.IsNullOrEmpty(config.DestinationPath))
            {
                Console.WriteLine("[ {0} ] The DestinationPath is emply.", config.Title);
                return;
            }
            // Folders
            try
            {
                string[] directories = Directory.GetDirectories(config.SourcePath, "*.*", SearchOption.AllDirectories);
                Parallel.ForEach(directories, dirPath =>
                {
                    var folderNow = dirPath.Replace(config.SourcePath + "\\", "");
                    var check = config.Ignore.Folders.FirstOrDefault(x => x == folderNow);
                    if (String.IsNullOrEmpty(check))
                    {
                        Console.WriteLine("[ {0} ] CreateDirectory  [ " + folderNow + " ]  ", config.Title);
                        Directory.CreateDirectory(dirPath.Replace(config.SourcePath, config.DestinationPath));
                    }
                });
            }
            catch (IOException) {
                Console.WriteLine("[ {0} ] The folder already exists.", config.Title);
            }
            catch (ArgumentNullException e) {
                Console.WriteLine("[ {0} ]" + e, config.Title);
            }
            catch (ArgumentException e) {
                Console.WriteLine("[ {0} ]" + e, config.Title);
            }
            catch (Exception e) {
                Console.WriteLine("[ {0} ]" + e, config.Title);
                throw;
            }

            // Files
            try
            {
                string[] files = Directory.GetFiles(config.SourcePath, "*.*", SearchOption.AllDirectories);
                Parallel.ForEach(files, newPath =>
                {
                    var fileNow = newPath.Replace(config.SourcePath + "\\", "");
                    var check = config.Ignore.Files.FirstOrDefault(x => x == fileNow);
                    if (String.IsNullOrEmpty(check))
                    {
                        try
                        {
                            File.Copy(newPath, newPath.Replace(config.SourcePath, config.DestinationPath));
                        }
                        catch (IOException e)
                        {
                            if (e.HResult == -2147024816)
                            {
                                Console.WriteLine("[ {0} ] The file '{1}' already exists.", config.Title, fileNow);
                            }
                            //throw;
                        }
                    }
                });
            }
            catch (IOException e)
            {
                if (e.HResult == -2147024816)
                {
                    Console.WriteLine("[ {0} ] The file already exists.", config.Title);
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("[ {0} ]" + e, config.Title);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("[ {0} ]" + e, config.Title);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ {0} ]" + e, config.Title);
                throw;
            }
        }
    }

}
