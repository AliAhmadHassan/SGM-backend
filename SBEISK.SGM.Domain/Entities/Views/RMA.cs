using System;

namespace SBEISK.SGM.Domain.Entities.Views
{
    public class RMA
    {
        public int RMAId { get; set; }
        public DateTime EmissionDate { get; set; }
        public string Status { get; set; }
        public string ApproverUser { get; set; }
        public string Installation { get; set; }
        public int ReceiverCode { get; set; }
        public string ReceiverName  { get; set; }
    }
}
