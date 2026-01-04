using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class InstallationTypeRepository : Repository<InstallationType>, IInstallationTypeRepository
    {
        public InstallationTypeRepository(SgmDataContext dataContext) : base(dataContext)
        {
        }

        public IList<InstallationType> WithItems()
        {
             return DataContext.InstallationTypes.ToList();
        }
    }
}
