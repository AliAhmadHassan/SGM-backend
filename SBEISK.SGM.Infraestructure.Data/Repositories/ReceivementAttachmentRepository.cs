using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementAttachmentRepository : Repository<ReceivementAttachment>, IReceivementAttachmentRepository
    {
        public ReceivementAttachmentRepository(SgmDataContext dataContext) : base(dataContext)
        {            
        }

        public void MergeAttachments(ICollection<ReceivementAttachment> original, ICollection<ReceivementAttachment> other, Action<ReceivementAttachment, ReceivementAttachment> updateStrategy)
        {
            Merger<ReceivementAttachment> merger = new Merger<ReceivementAttachment>((x, y) => x.Id == y.Id, (x, y) => x.ReceivementInvoiceOrderId == y.ReceivementInvoiceOrderId);
            MergeResult<ReceivementAttachment> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.receivementAttachments.RemoveRange(result.ItemsToDelete);
            this.DataContext.receivementAttachments.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }                         

    }
}