using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class MaterialMapping : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("MATERIAL");
            builder.Property(x => x.Id).HasColumnName("MAT_ID");
            builder.Property(x => x.Description).HasColumnName("MAT_DESCRICAO");
            builder.Property(x => x.Code).HasColumnName("MAT_COD");
            builder.Property(x => x.MaterialSubFamilyId).HasColumnName("MSF_ID");
            builder.Property(x => x.SbeiCode).HasColumnName("MAT_COD_SBEI");
            builder.Property(x => x.DeletedAt).HasColumnName("MAT_DATA_DELECAO");
            builder.Property(x => x.Unity).HasColumnName("MAT_UNIDADE");
            builder.Property(x => x.DeletedByProcedure).HasColumnName("MAT_DELECAO_PROCEDURE");
            builder.Property(x => x.UnitCost).HasColumnName("MAT_CUSTO_UNITARIO");

            builder.HasOne(x => x.SubFamily).WithOne(x => x.Material);
            builder.UseSoftDelete("MAT_DATA_DELECAO");
        }
    }
}