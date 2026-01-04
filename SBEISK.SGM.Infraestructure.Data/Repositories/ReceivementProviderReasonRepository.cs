using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementProviderReasonRepository : Repository<ReceivementProviderReason>, IReceivementProviderReasonRepository
    {
        private readonly IReceivementInvoiceOrderRepository receivementInvoiceWithoutOrderRepository;
        public ReceivementProviderReasonRepository(SgmDataContext dataContext, IReceivementInvoiceOrderRepository receivementInvoiceWithoutOrderRepository) : base(dataContext)
        {
            this.receivementInvoiceWithoutOrderRepository = receivementInvoiceWithoutOrderRepository;
        }

        public ReceivementProviderReason AddReceivementProviderReason(int? providerId, int? reasonId)
        {
            Provider provider = DataContext.Providers.FirstOrDefault(p => p.Id.Equals(providerId));
            ReasonWithoutOrder reason = DataContext.ReasonWithoutOrders.FirstOrDefault(r => r.Id.Equals(reasonId));
            
            ReceivementProviderReason recProviders = new ReceivementProviderReason();
            recProviders.ReceivementId = this.receivementInvoiceWithoutOrderRepository.LastIdReceiver();
            recProviders.ProviderId = null;
            if(provider != null)
            {
                recProviders.ProviderId = provider.Id;
            }
            recProviders.ReasonWithoutOrderId = null;
            if(reason != null)
            {
                recProviders.ReasonWithoutOrderId = reason.Id;
            }
            return recProviders;
        }
    }
}