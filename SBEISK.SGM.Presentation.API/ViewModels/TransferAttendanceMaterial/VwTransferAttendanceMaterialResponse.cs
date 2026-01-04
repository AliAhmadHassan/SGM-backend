using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SBEISK.SGM.Presentation.API.ViewModels.TransferAttendanceMaterial
{
    public class VwTransferAttendanceMaterialResponse: Domain.Entities.Views.VwTransferAttendanceMaterial
    {
        public List<MaterialSTMResponse> Materials { get; set; }
    }
}
