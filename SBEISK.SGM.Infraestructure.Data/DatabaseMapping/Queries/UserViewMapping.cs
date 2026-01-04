using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class UserViewMapping : IQueryTypeConfiguration<Users>
    {
        public void Configure(QueryTypeBuilder<Users> builder)
        {
            builder.ToView("VW_USUARIOS");
            builder.Property(x => x.Id).HasColumnName("USU_ID");
            builder.Property(x => x.Name).HasColumnName("USU_NOME");
            builder.Property(x => x.Active).HasColumnName("ATIVO");
            builder.Property(x => x.InstallationsProfiles).HasColumnName("INSTALACOES_PERFIS");
        }
    }
}
