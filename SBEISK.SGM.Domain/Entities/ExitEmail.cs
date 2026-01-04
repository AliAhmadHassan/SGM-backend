using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ExitEmail : Entity
    {
        public int DirectExitId { get; set; }
        public string Email { get; set; }
        public DirectExit DirectExit { get; set; }
    }
}