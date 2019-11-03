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
    },
    "Backup": {
      "Enable": "true",
      "SourcePath": "C:\\CopyBackup_Tool\\TEST\\Folder1",
      "MoveToPath": "C:\\CopyBackup_Tool\\TEST"
    }
  }
  {
    "Title": "Back",
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
    },
    "Backup": {
      "Enable": "true",
      "SourcePath": "C:\\CopyBackup_Tool\\TEST\\Folder1",
      "MoveToPath": "C:\\CopyBackup_Tool\\TEST"
    }
  }
]
```
