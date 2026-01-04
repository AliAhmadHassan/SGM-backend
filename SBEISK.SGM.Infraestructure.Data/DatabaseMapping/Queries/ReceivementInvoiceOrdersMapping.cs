using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class ReceivementInvoiceOrdersMapping : IQueryTypeConfiguration<ReceivementInvoiceOrders>
    {
        public void Configure(QueryTypeBuilder<ReceivementInvoiceOrders> builder)
        {
            builder.ToView("VW_RECEBIMENTO_NOTAFISCAL_PEDIDOS");
            builder.Property(x => x.BranchOfficeDescription).HasColumnName("FIL_DESCRICAO");
            builder.Property(x => x.InstallationId).HasColumnName("INS_ID");
            builder.Property(x => x.OrderCode).HasColumnName("PED_ID");
            builder.Property(x => x.ProviderName).HasColumnName("FOR_RAZAO_SOCIAL");
            builder.Property(x => x.OrderEmission).HasColumnName("PED_DT_EMISSAO");
            builder.Property(x => x.CNPJ).HasColumnName("FOR_CNPJ");
            builder.Property(x => x.OrderStatus).HasColumnName("PDS_DESCRICAO");
        }
    }
}