using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class UserInstallationsProfilesMapping : IQueryTypeConfiguration<UserInstallationsProfiles>
    {
        public void Configure(QueryTypeBuilder<UserInstallationsProfiles> builder)
        {
            builder.ToView("VW_INSTALACOES_USUARIO_PERFIL");
            builder.Property(x => x.UserId).HasColumnName("USU_ID");
            builder.Property(x => x.InstallationId).HasColumnName("INS_ID");
            builder.Property(x => x.ProfileId).HasColumnName("PER_ID");
            builder.Property(x => x.InstallationDescription).HasColumnName("INS_DESCRICAO");
            builder.Property(x => x.BranchOfficeId).HasColumnName("FIL_ID");
            builder.Property(x => x.BranchOfficeDescription).HasColumnName("FIL_DESCRICAO");
            builder.Property(x => x.Location).HasColumnName("LOCALIZACAO");
        }
    }
}
