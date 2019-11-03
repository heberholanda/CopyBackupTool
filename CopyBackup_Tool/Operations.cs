using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;

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
                    Console.WriteLine("[ Config ] Loading...");
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<List<FileModel>>(json);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("[ Config ] The file Config File not found!");
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw;
            }

        }

        // https://archive.codeplex.com/?p=dotnetzip#Zip/ZipFile.cs
        public void zipFolder(ZipFolder backup)
        {
            // Encode Type Set
            EncodingProvider provider = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(provider);

            if (backup.Enable != true) return;
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    var dateNow = DateTime.Now.ToString("dd-MM-yyyy HH-mm");
                    zip.AddDirectory(@backup.SourcePath);
                    zip.Comment = "Created: " + dateNow;
                    zip.Save(@backup.MoveToPath + "\\" + backup.ZipFileName + " - " + dateNow + ".zip");
                }
            }
            catch (Ionic.Zip.ZipException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (System.IO.FileNotFoundException)
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                        Directory.CreateDirectory(dirPath.Replace(config.SourcePath, config.DestinationPath));
                        Console.WriteLine("[ {0} ] CreateDirectory: {1}", config.Title, folderNow);
                    }
                });
            }
            catch (IOException) {
                Console.WriteLine("[ {0} ] The folder already exists.", config.Title);
            }
            catch (ArgumentNullException e) {
                Console.WriteLine("[ {0} ]" + e.Message, config.Title);
            }
            catch (ArgumentException e) {
                Console.WriteLine("[ {0} ]" + e.Message, config.Title);
            }
            catch (Exception e) {
                Console.WriteLine("[ {0} ]" + e.Message, config.Title);
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
                            Console.WriteLine("[ {0} ] CopyFile: {1}", config.Title, fileNow);
                        }
                        catch (IOException e)
                        {
                            if (e.HResult == -2147024816)
                            {
                                Console.WriteLine("[ {0} ] The file '{1}' already exists.", config.Title, fileNow);
                            }
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
                Console.WriteLine("[ {0} ]" + e.Message, config.Title);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("[ {0} ]" + e.Message, config.Title);
            }
            catch (Exception e)
            {
                Console.WriteLine("[ {0} ]" + e.Message, config.Title);
                throw;
            }
        }
    }

}
