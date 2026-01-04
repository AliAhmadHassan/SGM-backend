using System;

namespace SBEISK.SGM.Domain.Entities.Views
{
    public class RMAAttendances
    {
        public int RMAId { get; set; }
        public DateTime EmissionDate { get; set; }
        public string Status { get; set; }
        public string RequesterUser { get; set; }
        public string Installation { get; set; }
        public int ReceiverCode { get; set; }
        public string ReceiverName  { get; set; }
    }
}