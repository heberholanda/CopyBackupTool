using System.IO;

namespace CopyBackupTool.Models
{
    public class Ignore
    {
        public string[] Files;
        public string[] Folders;

        public string[] GetFiles() { return Files; }
        public string[] GetFolders() { return Folders; }
        public void SetFiles(string[] value) { Files = value; }
        public void SetFolders(string[] value) { Folders = value; }
    }
}
