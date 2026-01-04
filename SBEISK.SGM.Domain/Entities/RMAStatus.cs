using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class RMAStatus : Entity
    {
        public string Description { get; set; }

        public RequisitionOfMaterialForApplication RMA { get; set; }
    }
}
