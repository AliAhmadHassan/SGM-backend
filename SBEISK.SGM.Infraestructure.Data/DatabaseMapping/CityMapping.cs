using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("CIDADE");
            builder.Property(x => x.Id).HasColumnName("CID_ID");
            builder.Property(x => x.Name).HasColumnName("CID_NOME");
            builder.Property(x => x.UfId).HasColumnName("UF_ID");
            builder
                .HasOne<Uf>(s => s.Uf)
                .WithMany(g => g.Cities)
                .HasForeignKey(s => s.UfId);
        }
    }
}