# CopyBackup Tool - Move Files, Folders & Zip Folder

## Package Required

	1. Microsoft.NETCore.App
	2. Ionic.Zip
	3. Newtonsoft.Json

## Example Config
```
[
  {
    "Title": "Front",
    "Status": "true",
    "CompressFolder": {
      "Status": "true",
      "ZipFileName": "Front",
      "SourcePath": "C:\\CopyBackupTool\\CopyBackupTool\\TEST\\Folder1",
      "MoveToPath": "C:\\CopyBackupTool\\CopyBackupTool\\TEST",
      "Ignore": {
        "Folders": [],
        "Files": [ "1 file.txt", "Another\\1 file.txt" ]
      }
    },
    "CopyAndPaste": {
      "Status": "true",
      "SourcePath": "C:\\CopyBackupTool\\CopyBackupTool\\TEST\\Folder1",
      "DestinationPath": "C:\\CopyBackupTool\\CopyBackupTool\\TEST\\Folder2",
      "Ignore": {
        "Folders": [ "folder1", "folder2", "folder3", "Inside" ],
        "Files": []
      }
    }
  }
]
```
## Compiling
```Build your project and copy the configuration file "ConfigurationFile.json" so that it is next to the executable.```