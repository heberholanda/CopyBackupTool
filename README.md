# CopyBackup Tool - Move Files, Folders & Zip Folder
```
Load Config in Json file.
```

## Package Required

	1. Microsoft.NETCore.App
	2. System.Security.Permissions
	3. System.Text.Encoding.CodePages
	1. Ionic.Zip
	2. Newtonsoft.Json


## Example Config
```
[
  {
    "Title": "Front",
    "Backup": {
      "Enable": "true",
      "SourcePath": "C:\\CopyBackup_Tool\\TEST\\Folder1",
      "MoveToPath": "C:\\CopyBackup_Tool\\TEST"
    },
    "Enable": "true",
    "SourcePath": "C:\\CopyBackup_Tool\\TEST\\Folder1",
    "DestinationPath": "C:\\CopyBackup_Tool\\TEST\\Folder2",
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
    }
  }
  {
    "Title": "Back",
    "Backup": {
      "Enable": "true",
      "SourcePath": "C:\\CopyBackup_Tool\\TEST\\Folder1",
      "MoveToPath": "C:\\CopyBackup_Tool\\TEST"
    },
    "Enable": "true",
    "SourcePath": "C:\\CopyBackup_Tool\\TEST\\Folder1",
    "DestinationPath": "C:\\CopyBackup_Tool\\TEST\\Folder2",
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
    }
  }
]
```
## Compiling
```Build your project and copy the configuration file "ConfigurationFile.json" so that it is next to the executable.```
