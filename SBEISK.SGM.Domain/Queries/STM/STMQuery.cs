using System;

namespace SBEISK.SGM.Domain.Queries
{
    public class STMQuery
    {
        public int? STM { get; set; }
        public int? Transfer { get; set; }
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