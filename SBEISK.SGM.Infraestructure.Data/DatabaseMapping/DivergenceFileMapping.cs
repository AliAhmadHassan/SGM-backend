using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DivergenceFileMapping : IEntityTypeConfiguration<DivergenceFiles>
    {
        public void Configure(EntityTypeBuilder<DivergenceFiles> builder)
        {
            builder.ToTable("DIV_DOCUMENTO");
            builder.Property(x => x.Id).HasColumnName("DII_ID");
            builder.Property(x => x.Document).HasColumnName("DII_DOCUMENTO");
            builder.Property(x => x.DivergenceId).HasColumnName("DIV_ID"); 
            builder.Property(x => x.Name).HasColumnName("DII_NOME");
            builder
                .HasOne(x => x.Divergence)
                .WithMany(x => x.DivergenceFiles)
                .HasForeignKey(x => x.DivergenceId);
        }
    }
}