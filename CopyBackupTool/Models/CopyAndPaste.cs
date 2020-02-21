namespace CopyBackupTool.Models
{
    public class CopyAndPaste
    {
        public bool Status { get; set; }
        public bool Overwrite { get; set; }
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public Ignore Ignore { get; set; }
    }
}
