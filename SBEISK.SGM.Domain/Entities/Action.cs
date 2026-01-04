using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;


namespace SBEISK.SGM.Domain.Entities
{
    public class Action : Entity
    {
        public string Description { get; set; }
        public int? ParentActionId { get; set; }
        public Action ParentAction { get ; set ;}
        public ICollection<Action> ChildrenActions { get; set;}
        public ICollection<ProfileAction> ProfileActions { get; set; }
    }
}