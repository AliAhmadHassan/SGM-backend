using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementAttachmentRepository : IRepository<ReceivementAttachment>
    {
        void MergeAttachments(ICollection<ReceivementAttachment> original, ICollection<ReceivementAttachment> other, Action<ReceivementAttachment, ReceivementAttachment> updateStrategy);
    }
}