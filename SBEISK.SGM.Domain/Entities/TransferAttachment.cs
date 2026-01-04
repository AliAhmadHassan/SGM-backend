using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class TransferAttachment : Entity
    {
        public byte[] Document { get; set; }
        public bool Fiscal { get; set; }
        public int TransferId { get; set; }
        public Transfer Transfer { get; set; }
    }
}