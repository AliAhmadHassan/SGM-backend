using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReasonWithoutOrderMapping : IEntityTypeConfiguration<ReasonWithoutOrder>
    {
        public void Configure(EntityTypeBuilder<ReasonWithoutOrder> builder)
        {
            builder.ToTable("MOT_SEM_PEDIDO");
            builder.Property(x => x.Id).HasColumnName("MSP_ID");
            builder.Property(x => x.Description).HasColumnName("MSP_DESCRICAO");

            builder.HasMany(x => x.ReceivementProviderReasons).WithOne(x => x.ReasonWithoutOrder);
        }
    }
}