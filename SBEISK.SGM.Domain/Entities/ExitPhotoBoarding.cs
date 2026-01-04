using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ExitPhotoBoarding : Entity
    {
        public byte[] Photo { get; set; }
        public int DirectExitId { get; set; }
        public DirectExit DirectExit { get; set; }
    }
}