using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class RMADetailsMapping : IQueryTypeConfiguration<RMADetails>
    {
        public void Configure(QueryTypeBuilder<RMADetails> builder)
        {
         builder.ToView("VW_RMA_DETALHES");
         builder.Property(x => x.RMAId).HasColumnName("RMA"); 
         builder.Property(x => x.RequesterUser).HasColumnName("REQUISITANTE");  
         builder.Property(x => x.EmissionDate).HasColumnName("DT_EMISSAO");  
         builder.Property(x => x.Status).HasColumnName("STATUS");  
         builder.Property(x => x.ApproverUser).HasColumnName("APROVADOR");  
         builder.Property(x => x.ApprovementDate).HasColumnName("DT_APROVACAO");  
         builder.Property(x => x.Installation).HasColumnName("INS_ORIGEM");  
         builder.Property(x => x.ReceiverUser ).HasColumnName("RETIRADA");  
         builder.Property(x => x.ReceiverCode).HasColumnName("DES_ID");  
         builder.Property(x => x.ReceiverName).HasColumnName("DES_DESCRICAO");   
         builder.Property(x => x.ReceiverType).HasColumnName("TPD_DESCRICAO");   
         builder.Property(x => x.ReceiverAddress).HasColumnName("DES_ENDERECO");   
         builder.Property(x => x.Notes).HasColumnName("RMA_OBSERVACAO");  
        }
    }
}
