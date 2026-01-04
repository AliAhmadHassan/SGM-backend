using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DirectExitReceiverEmailMapping : IEntityTypeConfiguration<DirectExitReceiverEmail>
    {
        public void Configure(EntityTypeBuilder<DirectExitReceiverEmail> builder)
        {
            builder.ToTable("SAI_DEVOLUCAO_DESTINATARIO_EMAIL");
            builder.Property(x => x.Id).HasColumnName("DDE_ID");
            builder.Property(x => x.Email).HasColumnName("DDE_EMAIL");
            builder.Property(x => x.DirectExitReceiverId).HasColumnName("SAD_ID");

            builder.HasOne(x => x.DirectExitReceiver).WithMany().HasForeignKey(x => x.DirectExitReceiverId);
        }
    }
}