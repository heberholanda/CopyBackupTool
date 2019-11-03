# CopyBackup Tool - Move Files, Folders & Zip Folder
```
Load Config in Json file.
```
## Example Config
```
[
  {
    "Title": "Front",
    "Enable": "true",
    "SourcePath": "C:\\\\\\CopyBackup_Tool\\\\TESTE\\\\Pasta1",
    "DestinationPath": "C:\\CopyBackup_Tool\\TESTE\\Pasta2",
    "Ignore": {
      "Folders": [
        "folder1",
        "folder2",
        "folder3",
        "Inside"
      ],
      "Files": [
        "folder4/abc.txt",
        "folder4/xzd.dll"
      ]
    },
    "Backup": {
      "Enable": "true",
      "SourcePath": "C:\\CopyBackup_Tool\\TESTE\\Pasta1",
      "MoveToPath": "C:\\CopyBackup_Tool\\TESTE"
    }
  }
]
```
## Package Required

	1. Microsoft.NETCore.App
	2. System.Security.Permissions
	3. System.Text.Encoding.CodePages
	1. Ionic.Zip
	2. Newtonsoft.Json

## Compiling
```Build your project and copy the configuration file "ConfigurationFile.json" so that it is next to the executable.```
