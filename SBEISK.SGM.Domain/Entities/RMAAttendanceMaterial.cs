using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RMAAttendanceMaterial : Entity
    {
        public decimal Quantity { get; set; }
        public int RMAAttendanceID { get; set; }
        public int MAterialId { get; set; }
        public int RMAMaterialId { get; set; }
        public RMAAttendance RMAAttendance { get; set; }
        public Material Material { get; set; }
        public RMAMaterial RMAMaterial { get; set; }
    }
}