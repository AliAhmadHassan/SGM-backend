using System;
using System.Collections.Generic;
using System.Text;

namespace SBEISK.SGM.Domain.Queries.TransferAttendanceMaterial
{
    public class VwTransferAttendanceMaterialQuery
    {
        public int? STM { get; set; }
        public int? StatusStm { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? RequiredUser { get; set; }
        public int? WithdrawUser { get; set; }
        public int? InstallationSource { get; set; }
        public int? InstallationDestiny { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
    }
}
