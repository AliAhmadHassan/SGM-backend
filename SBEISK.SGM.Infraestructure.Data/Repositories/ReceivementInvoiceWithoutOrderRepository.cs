using System.Collections.Generic;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.Material;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementInvoiceWithoutOrderRepository : Repository<ReceivementInvoiceOrder>, IReceivementInvoiceWithoutOrderRepository
    {
        private readonly IMaterialRepository materialRepository;
        public ReceivementInvoiceWithoutOrderRepository(SgmDataContext dataContext, IMaterialRepository materialRepository) : base(dataContext)
        {
            this.materialRepository = materialRepository; 
        }

        public decimal CalculateTotalPrice(decimal amount, decimal price)
        {
            return price * amount;
        }
    }
}