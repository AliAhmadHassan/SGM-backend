using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class SolicitationStatusMapping : IEntityTypeConfiguration<SolicitationStatus>
    {
        public void Configure(EntityTypeBuilder<SolicitationStatus> builder)
        {
            builder.ToTable("STM_STATUS");
            builder.Property(x => x.Id).HasColumnName("STS_ID");
            builder.Property(x => x.Description).HasColumnName("STS_DESCRICAO");
            
            builder.HasMany(x => x.STMs).WithOne(x => x.SolicitationStatus);
        }
    }
}