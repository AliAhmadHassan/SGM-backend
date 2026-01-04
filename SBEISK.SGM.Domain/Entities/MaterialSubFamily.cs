using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class MaterialSubFamily : Entity
    {
        public string Description { get; set; }
        public int MaterialFamilyId { get; set; } 
        public MaterialFamily Family { get; set; }
        public Material Material { get; set; }
    }
}