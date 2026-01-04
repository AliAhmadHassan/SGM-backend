using Microsoft.AspNetCore.Http;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories.Base;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface IDivergenceFileRepository : IRepository<DivergenceFiles>
    {
        void SaveFiles(IFormFileCollection files, int id);
    }
}
