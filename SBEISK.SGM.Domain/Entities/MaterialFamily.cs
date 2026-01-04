using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class MaterialFamily : Entity
    {
        public string Description { get; set; }
        public MaterialSubFamily SubFamily { get; set; }
    }
}