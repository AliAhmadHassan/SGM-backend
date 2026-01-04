using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementEmailMapping : IEntityTypeConfiguration<ReceivementEmail>
    {
        public void Configure(EntityTypeBuilder<ReceivementEmail> builder)
        {
            builder.ToTable("REC_EMAIL");
            builder.Property(x => x.Id).HasColumnName("RCE_ID");
            builder.Property(x => x.ReceivementInvoiceOrderId).HasColumnName("REC_ID");
            builder.Property(x => x.Email).HasColumnName("REC_DESCRICAO");

            builder.HasOne(x => x.ReceivementInvoiceOrder).WithMany(x => x.ReceivementEmails)
                .HasForeignKey(x => x.ReceivementInvoiceOrderId);
        }
    }
}