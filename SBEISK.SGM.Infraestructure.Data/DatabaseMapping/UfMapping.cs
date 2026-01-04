using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class UfMapping : IEntityTypeConfiguration<Uf>
    {
        public void Configure(EntityTypeBuilder<Uf> builder)
        {
            builder.ToTable("UF");
            builder.Property(x => x.Id).HasColumnName("UF_ID");
            builder.Property(x => x.Initials).HasColumnName("UF_SIGLA");
            builder.Property(x => x.Name).HasColumnName("UF_NOME");
        }
    }
}