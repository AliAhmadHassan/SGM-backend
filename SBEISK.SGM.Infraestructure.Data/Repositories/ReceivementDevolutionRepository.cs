using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementDevolutionRepository : Repository<ReceivementDevolutionReceiver>, IReceivementDevolutionRepository
    {
        public ReceivementDevolutionRepository(SgmDataContext dataContext) : base(dataContext)
        {
            
        }
    }
}