using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAMaterialMapping : IEntityTypeConfiguration<RMAMaterial>
    {
        public void Configure(EntityTypeBuilder<RMAMaterial> builder)
        {
            builder.ToTable("RMA_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("RMM_ID");
            builder.Property(x => x.Quantity).HasColumnName("RMM_QUANTIDADE");
            builder.Property(x => x.RMAId).HasColumnName("RMA_ID");
            builder.Property(x => x.MaterialId).HasColumnName("MAT_ID");
            builder.Property(x => x.DisciplineId).HasColumnName("DIS_ID");
            builder.Property(x => x.AmountReceived).HasColumnName("RMM_RECEBIDO");

            builder.HasOne(x => x.RMA).WithMany(x => x.Materials).HasForeignKey(x => x.RMAId);
        }
    }
}