using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RMAMaterial : Entity
    {
        public decimal Quantity { get; set; }
        public int RMAId { get; set; }
        public int MaterialId { get; set; }
        public int DisciplineId { get; set; }
        public decimal AmountReceived { get; set; }
        public RequisitionOfMaterialForApplication RMA { get; set; }
        public Material Material { get; set; }
        public Discipline Discipline { get; set; }
        public List<RMAAttendanceMaterial> RMAAttendanceMaterial { get; set; }
    }
}
