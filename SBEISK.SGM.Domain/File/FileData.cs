using System;

namespace SBEISK.SGM.Domain.File
{
    public class FileData
    {
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string Extension { get; set; }

        public FileData(string extension, string folderName)
        {
            Extension = extension;
            FolderName = folderName;
            FileName = $"{Guid.NewGuid().ToString().Replace(".", "").Replace("-", "")}.{Extension}";
        }
    }
}