using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class InstallationsMapping : IQueryTypeConfiguration<Installations>
    {
        public void Configure(QueryTypeBuilder<Installations> builder)
        {
            builder.ToView("VW_INSTALACOES");
            builder.Property(x => x.Id).HasColumnName("INS_ID");
            builder.Property(x => x.Name).HasColumnName("INS_NOME");
            builder.Property(x => x.Description).HasColumnName("INS_DESCRICAO");
            builder.Property(x => x.TypeId).HasColumnName("TPI_ID");
            builder.Property(x => x.TypeDescription).HasColumnName("TPI_DESCRICAO");
            builder.Property(x => x.ProjectId).HasColumnName("PRO_ID");
            builder.Property(x => x.ProjectDescription).HasColumnName("PRO_DESCRICAO");
            builder.Property(x => x.AddressId).HasColumnName("END_ID");   
            builder.Property(x => x.Address).HasColumnName("ENDERECO");
            builder.Property(x => x.ThirdMaterial).HasColumnName("MATERIAL_TERCEIRO");
            builder.Property(x => x.IsThirdMaterial).HasColumnName("INS_MATERIAL_TERCEIRO");
            builder.UseSoftDelete("INS_DT_DELECAO");
        }
    }
}
