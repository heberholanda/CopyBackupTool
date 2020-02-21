using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CopyBackupTool;
using CopyBackupTool.Models;
using CopyBackupTool.Helpers;

namespace ConspiracaoCopy
{
    public class Operations : IOperations<FileModel>
    {
        public List<FileModel> JsonFileConfigs { get; set; }
        public string ConfigPath { get; set; }
        public string ConfigFile { get; set; }

        public Operations()
        {
            this.ConfigFile = "ConfigurationFile.json";
            //ConfigPath = AppDomain.CurrentDomain.BaseDirectory + ConfigFile;
            this.ConfigPath = "C:\\CopyBackupTool\\CopyBackupTool\\" + ConfigFile;
            this.JsonFileConfigs = new List<FileModel>();
            this.JsonFileConfigs = this.LoadJson();
        }

        public List<FileModel> LoadJson()
        {
            //var jsonTest = HelpersStatic.JsonIsValid(this.ConfigPath);
            
            try
            {
                using StreamReader reader = new StreamReader(this.ConfigPath);
                //bool _jsonIsValid = json.IsJsonValid<test>();

                Console.WriteLine("[ Config ] Loading...");
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<FileModel>>(json);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("[ Config ] The file Config File not found!");
                Console.WriteLine(ex.FileName);
                return new List<FileModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ Config ] " + ex.Message);
                Console.ReadKey();
                throw;
            }
        }
        public void CompressFolder(CompressFolder compress)
        {
            if (compress.Status != true) {
                Console.WriteLine("[ {0} ] Not enabled. Stoped! Status: ", "CompressFolder", compress.Status);
                return;
            }
            try
            {
                // https://archive.codeplex.com/?p=dotnetzip#Zip/ZipFile.cs
                // Encode Type Set
                EncodingProvider provider = CodePagesEncodingProvider.Instance;
                Encoding.RegisterProvider(provider);

                using (ZipFile zip = new ZipFile())
                {
                    string _dateNow = DateTime.Now.ToString("dd-MM-yyyy HH-mm");
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;

                    // Add Folders
                    var _foldersIgnore = new HashSet<string>(compress.Ignore.Folders);
                    var _foldersAll = Directory.GetDirectories(compress.SourcePath, "*", SearchOption.AllDirectories).ToList();
                    var _foldersWithoutIgnore = _foldersAll.Where(p => !_foldersIgnore.Contains(p.Replace(compress.SourcePath + "\\", ""))).ToList();
                    foreach (var _folder in _foldersWithoutIgnore)
                    {
                        var _folderName = _folder.Replace(compress.SourcePath + "\\", "");
                        zip.AddDirectory(_folder, _folderName);
                        //zip.AddSelectedFiles("*", _folder, true);
                        //zip.AddFile(_folder, Path.GetDirectoryName(_folder).Replace(folder.SourcePath, string.Empty));
                        Console.WriteLine("[ ZipFile ] {0} folder successfully added.", _folder);
                    }

                    // Add Files
                    var _filesIgnore = new HashSet<string>(compress.Ignore.Files);
                    var _filesAll = Directory.GetFiles(compress.SourcePath, "*", SearchOption.AllDirectories).ToList();
                    var _filesWithoutIgnore = _filesAll.Where(p => !_filesIgnore.Contains(p.Replace(compress.SourcePath + "\\", ""))).ToList();
                    foreach (var _file in _filesWithoutIgnore)
                    {
                        zip.AddFile(_file, Path.GetDirectoryName(_file).Replace(compress.SourcePath, string.Empty));
                        Console.WriteLine("[ ZipFile ] {0} file successfully added.", _file);
                    }

                    zip.Comment = "Created: " + _dateNow;
                    zip.Save(compress.MoveToPath + "\\" + compress.ZipFileName + " - " + _dateNow + ".zip");
                    Console.WriteLine("[ ZipFile ] {0}.zip successfully created.", compress.ZipFileName);
                }
            }
            catch (ZipException ex) {
                Console.WriteLine("[ ZipFile ] " + ex.Message);
            }
            catch (FileNotFoundException ex) {
                Console.WriteLine("[ ZipFile ] " + ex.Message);
            }
            catch (DirectoryNotFoundException ex) {
                System.Text.RegularExpressions.Regex pathMatcher = new System.Text.RegularExpressions.Regex(@"[^']+");
                Console.WriteLine("[ ZipFile ] Could not find a part of the path: " + pathMatcher.Matches(ex.Message)[1].Value);
            }
            catch (ArgumentException ex) {
                Console.WriteLine("[ ZipFile ] " + ex.Message);
            }
            catch (Exception ex) {
                Console.WriteLine("[ ZipFile ] " + ex.Message);
                throw;
            }
        }
        public void CopyAndPaste(CopyAndPaste copy)
        {
            if (copy.Status == false) {
                Console.WriteLine("[ {0} ] Not enabled. Stoped!", "Copy&Paste", copy.SourcePath);
                return;
            }
            if (String.IsNullOrEmpty(copy.SourcePath)) {
                Console.WriteLine("[ {0} ] The SourcePath ({1}) is emply. Stoped!", "Copy&Paste", copy.SourcePath);
                return;
            }
            if (String.IsNullOrEmpty(copy.DestinationPath)) {
                Console.WriteLine("[ {0} ] The DestinationPath ({1}) is emply. Stoped!", "Copy&Paste", copy.DestinationPath);
                return;
            }

            // Folders
            try
            {
                string[] directories = Directory.GetDirectories(copy.SourcePath, "*.*", SearchOption.AllDirectories);
                _ = Parallel.ForEach(directories, currentPath =>
                {
                    string _folderNow = currentPath.Replace(copy.SourcePath + "\\", "");
                    var _folderIgnoreCheck = copy.Ignore.GetFolders().FirstOrDefault(x => x == _folderNow);
                    if (String.IsNullOrEmpty(_folderIgnoreCheck))
                    {
                        Directory.CreateDirectory(currentPath.Replace(copy.SourcePath, copy.DestinationPath));
                        Console.WriteLine("[ {0} ] Create Directory: {1}", "Copy&Paste", _folderNow);
                    }
                    else
                    {
                        Console.WriteLine("[ {0} ] Folder was ignored: {1}", "Copy&Paste", _folderNow);
                    }
                });
            }
            catch (IOException)
            {
                Console.WriteLine("[ {0} ] The folder already exists.", "Copy&Paste");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("[ {0} ] " + ex.Message, "Copy&Paste");
            }

            catch (ArgumentException ex)
            {
                Console.WriteLine("[ {0} ] " + ex.Message, "Copy&Paste");
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("[ {0} ] " + ex.Message, "Copy&Paste");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ {0} ] " + ex.Message, "Copy&Paste");
                throw;
            }

            // Files
            try
            {
                string[] files = Directory.GetFiles(copy.SourcePath, "*.*", SearchOption.AllDirectories);
                Parallel.ForEach(files, newPath =>
                {
                    var _fileNow = newPath.Replace(copy.SourcePath + "\\", "");
                    var _fileIgnoreCheck = copy.Ignore.GetFiles().FirstOrDefault(x => x == _fileNow);
                    if (String.IsNullOrEmpty(_fileIgnoreCheck))
                    {
                        try
                        {
                            File.Copy(newPath, newPath.Replace(copy.SourcePath, copy.DestinationPath), copy.Overwrite);
                            Console.WriteLine("[ {0} ] CopyFile: {1}  Need replace? {2}", "Copy&Paste", _fileNow, copy.Overwrite);
                        }
                        catch (IOException e)
                        {
                            if (e.HResult == -2147024816)
                            {
                                Console.WriteLine("[ {0} ] The file '{1}' already exists.", "Copy&Paste", _fileNow);
                            }
                        }
                    } else
                    {
                        Console.WriteLine("[ {0} ] The file '{1}' was ignored.", "Copy&Paste", _fileNow);
                    }
                });
            }
            catch (IOException e)
            {
                if (e.HResult == -2147024816)
                {
                    Console.WriteLine("[ {0} ] The file already exists.", "Copy&Paste");
                }
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("[ {0} ] " + e.Message, "Copy&Paste");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("[ {0} ] " + e.Message, "Copy&Paste");
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("[ {0} ] " + ex.Message, "Copy&Paste");
            }
            catch (Exception e)
            {
                Console.WriteLine("[ {0} ] " + e.Message, "Copy&Paste");
                throw;
            }
        }
    }
}
