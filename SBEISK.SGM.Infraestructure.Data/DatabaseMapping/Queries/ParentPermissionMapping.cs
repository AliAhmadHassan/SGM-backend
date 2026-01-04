using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class ParentPermissionsMapping : IQueryTypeConfiguration<ParentPermissions>
    {
        public void Configure(QueryTypeBuilder<ParentPermissions> builder)
        {
            builder.ToView("VW_PERMISSAO_PAI");
            builder.Property(x => x.Id).HasColumnName("ACA_ID");
            builder.Property(x => x.Description).HasColumnName("ACA_DESCRICAO");
            builder.Property(x => x.ParentId).HasColumnName("ACA_ID_PAI");
            builder.Property(x => x.ParentDescription).HasColumnName("ACA_DESCRICAO_PAI"); 
        }
    }
}
