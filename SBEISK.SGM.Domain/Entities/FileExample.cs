using SBEISK.SGM.Domain.File;

namespace SBEISK.SGM.Domain.Entities
{
    public class FileExample : IFileStorable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }

        private const string FOLDER_NAME = "FILE_EXAMPLE";
        public FileData GetFileData(string extension)
        {
            return new FileData(extension, FOLDER_NAME);
        }
    }
}
