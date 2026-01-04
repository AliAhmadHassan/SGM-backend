using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IReceivementEmailRepository : IRepository<ReceivementEmail>
    {
       IEnumerable<object> AddEmail(string emails, int type);

       void MergeReceivementsEmail(ICollection<ReceivementEmail> original, ICollection<ReceivementEmail> other, Action<ReceivementEmail, ReceivementEmail> updateStrategy);
    }
}