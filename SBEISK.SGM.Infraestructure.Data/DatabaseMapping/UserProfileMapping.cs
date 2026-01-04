using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class UserProfileMapping : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("PERFIL");
            builder.Property(p => p.Id).HasColumnName("PER_ID");
            builder.Property(p => p.Name).HasColumnName("PER_NOME");
            builder.Property(p => p.Description).HasColumnName("PER_DESCRICAO");
            builder.Property(x => x.CreatedAt).HasColumnName("PER_DT_CRIACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("PER_DT_ATUALIZACAO");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.UseSoftDelete("PER_DT_DELECAO");
        }
    }
}