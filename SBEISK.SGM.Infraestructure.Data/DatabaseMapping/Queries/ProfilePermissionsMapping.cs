using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace  SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class ProfilePermissionsMapping : IQueryTypeConfiguration<ProfilePermissions>
    {
        public void Configure(QueryTypeBuilder<ProfilePermissions> builder)
        {
            builder.ToView("VW_PERFIL_PERMISSOES");
            builder.Property(x => x.ProfileId).HasColumnName("PER_ID");
            builder.Property(x => x.ProfileName).HasColumnName("PER_NOME");
            builder.Property(x => x.ProfileDescription).HasColumnName("PER_DESCRICAO");
            builder.Property(x => x.Permissions).HasColumnName("PERMISSOES");
            builder.Property(x => x.DeletedAt).HasColumnName("PER_DT_DELECAO");
            builder.UseSoftDelete("PER_DT_DELECAO");
        }
    }
}