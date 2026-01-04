using System;

namespace SBEISK.SGM.Domain.Entities.Views
{
    public class RMADetails
    {
        public int RMAId { get; set; }
        public string RequesterUser  { get; set; }
        public DateTime EmissionDate { get; set; }
        public string Status { get; set; }
        public string ApproverUser { get; set; }
        public DateTime ApprovementDate { get; set; }
        public string Installation { get; set; }
        public string ReceiverUser  { get; set; }
        public int ReceiverCode { get; set; }
        public string ReceiverName  { get; set; }
        public string ReceiverType  { get; set; }
        public string ReceiverAddress  { get; set; }
        public string Notes  { get; set; }
    }
}
