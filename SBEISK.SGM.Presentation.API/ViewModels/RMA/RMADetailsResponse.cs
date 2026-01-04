using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Presentation.API.ViewModels.RMA
{
    public class RMADetailsResponse : RMADetails
    {
         public List<MaterialRMAResponse> Materials { get; set; }
    }
}
