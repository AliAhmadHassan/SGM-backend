using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class InstallationTypeMapping : IEntityTypeConfiguration<InstallationType>
    {
        public void Configure(EntityTypeBuilder<InstallationType> builder)
        {
            builder.ToTable("TIP_INSTALACAO");
            builder.Property(x => x.Id).HasColumnName("TPI_ID");
            builder.Property(x => x.Description).HasColumnName("TPI_DESCRICAO");
        }
    }
}
