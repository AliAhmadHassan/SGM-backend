using System;
using System.IO;
using Microsoft.Extensions.Options;
using SBEISK.SGM.CrossCutting.Configurations;
using SBEISK.SGM.Domain.File;
using SBEISK.SGM.Infraestructure.Data.Storage.Base;

namespace SBEISK.SGM.Infraestructure.Data.Storage
{
    public class FileStorage : IStorage
    {
        private readonly string rootPath;
        
        public FileStorage(IOptions<FileSystem> options)
        {
            if (options == null || options.Value == null)
            {
                throw new InvalidOperationException($"{nameof(options)} must be a valid {typeof(FileSystem).Name} configuration.");
            }

            this.rootPath = options.Value.Path;
        }

        public void DeleteIfExists(string fileName)
        {
            string filePath = $"{rootPath}/{fileName}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public byte[] GetFileBytes(string folderName, string fileName)
        {
            ValidatePathParam(folderName, nameof(folderName));
            ValidatePathParam(fileName, nameof(fileName));
            string filePath = $"{rootPath}/{folderName}/{fileName}";
            byte[] buffer = null;
            using (FileStream stream = File.OpenRead(filePath))
            {
                buffer = FromStreamToByteArray(stream);
                stream.Close();
            }
            return buffer;
        }

        public Stream GetFileStream(string folderName, string fileName)
        {
            ValidatePathParam(folderName, nameof(folderName));
            ValidatePathParam(fileName, nameof(fileName));
            string filePath = $"{rootPath}/{folderName}/{fileName}";
            if (File.Exists(filePath))
            {
                return File.OpenRead(filePath);
            }
            return null;
        }

        private void ValidatePathParam(string param, string paramName)
        {
            if (param.Contains("/"))
            {
                throw new ArgumentException($"Param {paramName} invalid.");
            }
        }
        
        private byte[] FromStreamToByteArray(FileStream stream)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                stream.CopyTo(mStream);
                return mStream.ToArray();
            }
        }

        public string SaveFile(IFileStorable file, Stream incomingStream, string extension)
        {
            FileData fileData = file.GetFileData(extension);
            string folderPath = $"{rootPath}/{fileData.FolderName}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (Stream fileStream = File.Create($"{folderPath}/{fileData.FileName}"))
            {
                incomingStream.Seek(0, SeekOrigin.Begin);
                incomingStream.CopyTo(fileStream);
                return fileData.FileName;
            }
        }
    }
}
