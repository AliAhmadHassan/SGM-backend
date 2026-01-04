using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class MaterialSubFamilyMapping : IEntityTypeConfiguration<MaterialSubFamily>
    {
        public void Configure(EntityTypeBuilder<MaterialSubFamily> builder)
        {
            builder.ToTable("MAT_SUB_FAMILIA");
            builder.Property(x => x.Id).HasColumnName("MSF_ID");
            builder.Property(x => x.Description).HasColumnName("MSF_DESCRICAO");
            builder.Property(x => x.MaterialFamilyId).HasColumnName("MTF_ID");
            builder.HasOne(x => x.Family).WithOne(x => x.SubFamily);
        }
    }
}