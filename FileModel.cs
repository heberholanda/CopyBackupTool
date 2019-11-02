namespace ConspiracaoCopy
{
    public class FileModel
    {
        public string Title { get; set; }
        public bool Enable { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public IgnoreList Ignore { get; set; }
        public ZipFolder Backup { get; set; }
    }

    public class IgnoreList
    {
        public string[] Files { get; set; }
        public string[] Folders { get; set; }
    }

    public class ZipFolder
    {
        public bool Enable { get; set; }
        public string SourcePath { get; set; }
        public string MoveToPath { get; set; }
    }
}
