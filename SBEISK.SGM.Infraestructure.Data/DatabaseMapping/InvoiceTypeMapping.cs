using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class InvoiceTypeMapping : IEntityTypeConfiguration<InvoiceType>
    {
        public void Configure(EntityTypeBuilder<InvoiceType> builder)
        {
            builder.ToTable("TIP_FISCAL");
            builder.Property(x => x.Id).HasColumnName("TPF_ID");
            builder.Property(x => x.Description).HasColumnName("TPF_DESCRICAO");

            builder.HasMany(x => x.ReceivementInvoiceOrders).WithOne(x => x.InvoiceType);
        }
    }
}