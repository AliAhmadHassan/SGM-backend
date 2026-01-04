using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class UserPermissionMapping : IQueryTypeConfiguration<UserPermission>
    {
        public void Configure(QueryTypeBuilder<UserPermission> builder)
        {
            builder.ToView("VW_PERMISSOES_USUARIO");
            builder.Property(x => x.UserId).HasColumnName("USU_ID");
            builder.Property(x => x.installationId).HasColumnName("INS_ID");
            builder.Property(x => x.ProfileId).HasColumnName("PER_ID");
            builder.Property(x => x.PermissionId).HasColumnName("ACA_ID");
        }
    }
}
