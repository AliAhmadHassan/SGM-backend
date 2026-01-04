using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReasonOfTemporaryCustodyMapping : IEntityTypeConfiguration<ReasonOfTemporaryCustody>
    {
        public void Configure(EntityTypeBuilder<ReasonOfTemporaryCustody> builder)
        {
            builder.ToTable("MOT_GUA_PROVISORIA");
            builder.Property(x => x.Id).HasColumnName("MGP_ID");
            builder.Property(x => x.Description).HasColumnName("MGP_DESCRICAO");
        }
    }
}