using System;
using System.Collections.Generic;
using SBEISK.SGM.Presentation.API.ViewModels.Provider;

namespace SBEISK.SGM.Presentation.API.ViewModels.Order
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Emission { get; set; }
        public int Status { get; set; }
        public ProviderViewModel Provider { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
        
    }
}
