using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;
using SBEISK.SGM.Domain.File;

namespace SBEISK.SGM.Domain.Entities
{
    public class DivergenceFiles : Entity, IFileStorable
    {
        public byte[] Document { get; set; }
        public int DivergenceId { get; set; }
        public string Name { get; set; }
        public Divergence Divergence { get; set; }

        private const string FOLDER_NAME = "FILE_DIVERGENCE";
        public FileData GetFileData(string extension)
        {
            return new FileData(extension, FOLDER_NAME);
        }
    }
}