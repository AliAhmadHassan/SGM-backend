using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class DirectExitRepository : Repository<DirectExit>, IDirectExitRepository
    {
        public DirectExitRepository(SgmDataContext dataContext) : base(dataContext)
        {
            
        }

        public bool ValidateDate(DirectExit directExit)
        {
            if(directExit.DepartureDate > directExit.PrevisionDate)
                return false;
            return true;
        }
    }
}