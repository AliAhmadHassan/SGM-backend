using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ExitMaterialMapping : IEntityTypeConfiguration<ExitMaterial>
    {
        public void Configure(EntityTypeBuilder<ExitMaterial> builder)
        {
            builder.ToTable("SAIDA_DIRETA_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("SDM_ID");
            builder.Property(x => x.Amount).HasColumnName("SDM_QUANTIDADE");
            builder.Property(x => x.DirectExitId).HasColumnName("SDD_ID");
            builder.Property(x => x.MaterialId).HasColumnName("MAT_ID");

            builder.HasOne(x => x.DirectExit).WithMany(x => x.ExitMaterials).HasForeignKey(x => x.DirectExitId);
            builder.HasOne(x => x.Material).WithMany().HasForeignKey(x => x.MaterialId);
        }
    }
}