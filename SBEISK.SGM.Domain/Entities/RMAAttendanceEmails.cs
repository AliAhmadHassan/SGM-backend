using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RMAAttendanceEmails : Entity
    {
        public string Email  { get; set; }
        public int RMAAttendanceId { get; set; }
        public RMAAttendance RMAAttendance { get; set; }
    }
}