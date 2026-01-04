using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ExitTypeMapping : IEntityTypeConfiguration<ExitType>
    {
        public void Configure(EntityTypeBuilder<ExitType> builder)
        {
            builder.ToTable("TIP_SAIDA_DIRETA");
            builder.Property(x => x.Id).HasColumnName("TSD_ID");
            builder.Property(x => x.Description).HasColumnName("TSD_DESCRICAO");

            builder.HasMany(x => x.DirectExits).WithOne(x => x.ExitType);
        }
    }
}