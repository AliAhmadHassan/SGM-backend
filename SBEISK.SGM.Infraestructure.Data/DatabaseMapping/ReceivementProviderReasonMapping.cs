using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementProviderReasonMapping : IEntityTypeConfiguration<ReceivementProviderReason>
    {
        public void Configure(EntityTypeBuilder<ReceivementProviderReason> builder)
        {
            builder.ToTable("REC_SEM_PEDIDO_FORNECEDOR_MOTIVO");
            builder.Property(x => x.Id).HasColumnName("RFM_ID");
            builder.Property(x => x.ReceivementId).HasColumnName("REC_ID");
            builder.Property(x => x.ProviderId).HasColumnName("FOR_ID");
            builder.Property(x => x.ReasonWithoutOrderId).HasColumnName("MSP_ID");

            builder.HasOne(x => x.Receivement).WithOne(x => x.ReceivementProviderReasons);
            builder.HasOne(x => x.Provider).WithMany(x => x.ReceivementProviderReasons);
            builder.HasOne(x => x.ReasonWithoutOrder).WithMany(x => x.ReceivementProviderReasons);
        }
    }
}