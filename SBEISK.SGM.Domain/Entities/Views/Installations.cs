using SBEISK.SGM.Domain.Entities.Base;
using System;

namespace SBEISK.SGM.Domain.Entities.Views
{
    public class Installations : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ThirdMaterial { get; set; }
        public bool IsThirdMaterial { get; set; }
        public int TypeId { get; set; }
        public string TypeDescription { get; set; }
        public int ProjectId { get; set; }
        public string ProjectDescription { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public DateTime? DeletedAt { get; set; }        
    }
}
