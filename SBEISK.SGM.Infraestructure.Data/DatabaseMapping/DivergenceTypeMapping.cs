using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DivergenceTypeMapping : IEntityTypeConfiguration<DivergenceType>
    {
        public void Configure(EntityTypeBuilder<DivergenceType> builder)
        {
            builder.ToTable("DIV_STATUS");
            builder.Property(x => x.Id).HasColumnName("DIS_ID");
            builder.Property(x => x.Description).HasColumnName("DIS_DESCRICAO");
        }
    }
}