namespace CopyBackupTool.Models
{
    public class CompressFolder
    {
        public bool Status { get; set; }
        public string ZipFileName { get; set; }
        public string SourcePath { get; set; }
        public string MoveToPath { get; set; }
        public Ignore Ignore { get; set; }
    }
}
