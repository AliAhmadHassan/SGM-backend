using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class MaterialStatusMapping : IEntityTypeConfiguration<MaterialStatus>
    {
        public void Configure(EntityTypeBuilder<MaterialStatus> builder)
        {
            builder.ToTable("MAT_STATUS");
            builder.Property(x => x.Id).HasColumnName("MTS_ID");
            builder.Property(x => x.Description).HasColumnName("MTS_DESCRICAO");
        }
    }
}