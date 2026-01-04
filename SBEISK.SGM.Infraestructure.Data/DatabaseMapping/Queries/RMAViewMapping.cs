using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class RMAViewMapping : IQueryTypeConfiguration<RMA>
    {
        public void Configure(QueryTypeBuilder<RMA> builder)
        {
            builder.ToView("VW_RMA");
            builder.Property(x => x.RMAId).HasColumnName("RMA");
            builder.Property(x => x.EmissionDate).HasColumnName("RMA_DT_CRIACAO");
            builder.Property(x => x.Status).HasColumnName("STATUS");
            builder.Property(x => x.ApproverUser).HasColumnName("APROVADOR");
            builder.Property(x => x.Installation).HasColumnName("INS_ORIGEM");
            builder.Property(x => x.ReceiverCode).HasColumnName("DES_ID");
            builder.Property(x => x.ReceiverName).HasColumnName("DES_DESCRICAO");
        }
    }
}
