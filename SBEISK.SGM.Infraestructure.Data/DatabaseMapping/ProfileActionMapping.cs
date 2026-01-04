using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ProfileActionMapping : IEntityTypeConfiguration<ProfileAction>
    {
        public void Configure(EntityTypeBuilder<ProfileAction> builder)
        {
            builder.ToTable("PERFIL_ACAO");
            builder.Property(x => x.ActionId).HasColumnName("ACA_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("PER_DT_CRIACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("PER_DT_ATUALIZACAO");
            builder.Property(x => x.Id).HasColumnName("PFA_ID");
            builder.Property(x => x.ProfileId).HasColumnName("PER_ID");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.HasOne(x => x.Action).WithMany(x => x.ProfileActions);
            builder.HasOne(x => x.Profile).WithMany(x => x.ProfileActions);
            
        }
    }
}
