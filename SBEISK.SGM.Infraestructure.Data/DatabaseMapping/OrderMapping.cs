using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("PEDIDO");
            builder.Property(x => x.Emission).HasColumnName("PED_DT_EMISSAO");
            builder.Property(x => x.Id).HasColumnName("PED_ID");
            builder.Property(x => x.InstalationId).HasColumnName("INS_ID");
            builder.Property(x => x.ProviderId).HasColumnName("FOR_ID");
            builder.Property(x => x.OrderStatusId).HasColumnName("PDS_ID");            
            builder.Property(x => x.SbeiCode).HasColumnName("PED_COD_SBEI");
            builder.Property(x => x.DeletedAt).HasColumnName("PED_DATA_DELECAO");
            builder.HasOne(x => x.Provider).WithMany(x => x.Orders);
            builder.HasOne(x => x.OrderStatus);
        }
    }
}
