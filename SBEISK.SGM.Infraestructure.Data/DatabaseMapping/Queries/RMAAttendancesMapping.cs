using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class RMAAttendancesMapping : IQueryTypeConfiguration<RMAAttendances>
    {
        public void Configure(QueryTypeBuilder<RMAAttendances> builder)
        {
            builder.ToView("VW_RMA_STATUS");
            builder.Property(x => x.RMAId).HasColumnName("RMA_ID");
            builder.Property(x => x.EmissionDate).HasColumnName("RMA_DT_CRIACAO");
            builder.Property(x => x.Status).HasColumnName("STATUS");
            builder.Property(x => x.RequesterUser).HasColumnName("REQUISITANTE");
            builder.Property(x => x.Installation).HasColumnName("INS_ORIGEM");
            builder.Property(x => x.ReceiverCode).HasColumnName("DES_ID");
            builder.Property(x => x.ReceiverName).HasColumnName("DES_DESCRICAO");
        }
    }
}