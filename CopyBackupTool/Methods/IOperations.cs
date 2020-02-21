using CopyBackupTool.Models;
using System.Collections.Generic;

namespace CopyBackupTool
{
    public interface IOperations<T>
    {
        List<FileModel> JsonFileConfigs { get; set; }

        public List<FileModel> LoadJson();
        public void CopyAndPaste(CopyAndPaste copy);
        public void CompressFolder(CompressFolder folder);
    }
}
