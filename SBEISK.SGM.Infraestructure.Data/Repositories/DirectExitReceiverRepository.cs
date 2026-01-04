using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class DirectExitReceiverRepository : Repository<DirectExitReceiver>, IDirectExitReceiverRepository
    {
        public DirectExitReceiverRepository(SgmDataContext dataContext) : base(dataContext)
        {            
        }

        public int LastIdDirectExit()
        {
            DirectExitReceiver exitReceiver = DbSet.LastOrDefault();

            if(exitReceiver != default(DirectExitReceiver))
                return exitReceiver.Id;
            return default(int);
        }
    }
}