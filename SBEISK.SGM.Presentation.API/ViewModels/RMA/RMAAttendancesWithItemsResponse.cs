using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Presentation.API.ViewModels.RMA
{
    public class RMAAttendancesWithItemsResponse : RMAAttendances
    {
        public IList<MaterialRequisition> Materials { get; set; }
    }
}