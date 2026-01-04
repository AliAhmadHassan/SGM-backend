using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;


namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class RMARDetailseadOnlyRepository : ReadOnlyRepository<RMADetails>, IRMADetailsReadOnlyRepository
    {
        public RMARDetailseadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {
        }

        public RMADetails GetRMA(int id)
        {
            return base.Query().FirstOrDefault(x => x.RMAId == id);
        }
    }
}