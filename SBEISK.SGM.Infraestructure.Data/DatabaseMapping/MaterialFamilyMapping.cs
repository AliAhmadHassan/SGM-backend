using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class MaterialFamilyMapping : IEntityTypeConfiguration<MaterialFamily>
    {
        public void Configure(EntityTypeBuilder<MaterialFamily> builder)
        {
            builder.ToTable("MAT_FAMILIA");
            builder.Property(x => x.Id).HasColumnName("MTF_ID");
            builder.Property(x => x.Description).HasColumnName("MTF_DESCRICAO");
        }
    }
}