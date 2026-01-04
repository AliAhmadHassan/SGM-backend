using System;
using System.Collections.Generic;
namespace SBEISK.SGM.Presentation.API.ViewModels.RMA
{
    public class RMAResponse : Domain.Entities.Views.RMA
    {
        public List<MaterialRMAResponse> Materials { get; set; }
    }
}
