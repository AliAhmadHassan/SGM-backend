using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementFileRepository : IRepository<ReceivementPhoto>
    {
        IEnumerable<object> UploadFile(IList<IFormFile> file, object generic);

        void MergePhotos(ICollection<ReceivementPhoto> original, ICollection<ReceivementPhoto> other, Action<ReceivementPhoto, ReceivementPhoto> updateStrategy);
    }
}