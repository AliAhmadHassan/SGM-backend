using System;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RMAattachments : Entity
    {
        public byte[] File { get; set; }
        public int RMAId { get; set; }
        public RequisitionOfMaterialForApplication RMA { get; set; }
    }
}
