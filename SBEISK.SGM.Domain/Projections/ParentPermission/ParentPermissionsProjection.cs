using SBEISK.SGM.Domain.Entities;
using System.Collections.Generic;

namespace SBEISK.SGM.Domain.Projections
{
    public class ParentPermissionsProjection
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IList<Action> Actions { get; set; }
    }
}