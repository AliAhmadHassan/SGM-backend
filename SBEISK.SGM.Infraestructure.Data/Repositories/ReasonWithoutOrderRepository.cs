using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReasonWithoutOrderRepository : Repository<ReasonWithoutOrder>, IReasonWithoutOrderRepository
    {
        public ReasonWithoutOrderRepository(SgmDataContext dataContext) : base(dataContext)
        {            
        }
    }
}