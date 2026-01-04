using System;

namespace SBEISK.SGM.Domain.Queries
{
    public class RMAQuery
    {
        public int? RMA { get; set; }
        public DateTime? InitialDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string Status { get; set; }
        public string User { get; set; }
        public string InstallationSource { get; set; }
        public int? ReceiverCode { get; set; }
        public string ReceiverName { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialDescription { get; set; }
    }
}