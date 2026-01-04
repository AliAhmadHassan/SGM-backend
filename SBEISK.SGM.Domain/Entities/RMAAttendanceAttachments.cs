using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RMAAttendanceAttachments : Entity
    {
        public byte[] Files { get; set; }
        public int RMAAttendanceId { get; set; }
        public RMAAttendance RMAattendance { get; set; }

    }
}