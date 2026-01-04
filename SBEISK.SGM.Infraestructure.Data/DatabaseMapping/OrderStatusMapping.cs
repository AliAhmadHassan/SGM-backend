using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class OrderStatusMapping : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("PED_STATUS");
            builder.Property(p => p.Id).HasColumnName("PDS_ID");
            builder.Property(p => p.Description).HasColumnName("PDS_DESCRICAO");
        }
    }
}