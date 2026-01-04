using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DivergenceMapping : IEntityTypeConfiguration<Divergence>
    {
        public void Configure(EntityTypeBuilder<Divergence> builder)
        {
            builder.ToTable("DIVERGENCIA");
            builder.Property(x => x.Id).HasColumnName("DIV_ID");
            builder.Property(x => x.StatusId).HasColumnName("DIV_STATUS");
            builder.Property(x => x.Description).HasColumnName("DIV_DESCRICAO"); 
            builder.Property(x => x.Note).HasColumnName("DIV_OBSERVACAO");
            builder.Property(x => x.ReceivementId).HasColumnName("REC_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("DIV_DT_CRIACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("DIV_DT_ATUALIZACAO");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.UseTimestamped("DIV_DT_CRIACAO","DIV_DT_ATUALIZACAO");
            builder.UseUserModel();

        }
    }
}
