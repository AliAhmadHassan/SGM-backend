using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("PEDIDO_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("PDM_ID");
            builder.Property(x => x.Quantity).HasColumnName("PDM_QUANTIDADE");
            builder.Property(x => x.OrderId).HasColumnName("PED_ID");
            builder.Property(x => x.MaterialId).HasColumnName("MAT_ID");
            builder.Property(x => x.SequenceNumber).HasColumnName("PDM_NUMERO_SEQUENCIAL");
            builder.Property(x => x.UnitaryCost).HasColumnName("PDM_PRECO_UNITARIO");
            builder.Property(x => x.DeletedAt).HasColumnName("PDM_DATA_DELECAO");
            builder.Property(x => x.AmountReceived).HasColumnName("PDM_RECEBIDO");
            builder.Property(x => x.ProcedureDeleted).HasColumnName("PDM_DELECAO_PROCEDURE");

            builder.UseSoftDelete("PDM_DATA_DELECAO");
            builder.HasOne(x => x.Order).WithMany(x => x.Items);
            builder.HasOne(x => x.Material).WithMany(x => x.OrderItems);
        }
    }
}
