using SBEISK.SGM.Domain.File;
using System.IO;

namespace SBEISK.SGM.Infraestructure.Data.Storage.Base
{
    public interface IStorage
    {
        void DeleteIfExists(string fileName);
        string SaveFile(IFileStorable file, Stream incomingStream, string extension);
        Stream GetFileStream(string folderName, string fileName);
        byte[] GetFileBytes(string folderName, string fileName);
    }
}
