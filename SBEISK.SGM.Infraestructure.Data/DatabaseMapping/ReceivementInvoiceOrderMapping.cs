using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementInvoiceOrderMapping : IEntityTypeConfiguration<ReceivementInvoiceOrder>
    {
        public void Configure(EntityTypeBuilder<ReceivementInvoiceOrder> builder)
        {
            builder.ToTable("RECEBIMENTO_NOTAFISCAL_PEDIDO");
            builder.Property(x => x.Id).HasColumnName("REC_ID");
            builder.Property(x => x.IsDraft).HasColumnName("REC_RASCUNHO");
            builder.Property(x => x.CreatedAt).HasColumnName("REC_DT_CRIACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("REC_DT_ATUALIZACAO");
            builder.Property(x => x.Invoice).HasColumnName("REC_NR_DOC_FISCAL");
            builder.Property(x => x.InvoiceCreatedAt).HasColumnName("REC_DT_DOC_FISCAL");
            builder.Property(x => x.ReceivementDate).HasColumnName("REC_DT_RECEBIMENTO");
            builder.Property(x => x.Complement).HasColumnName("REC_COMPLEMENTO");
            builder.Property(x => x.Note).HasColumnName("REC_OBSERVACAO");
            builder.Property(x => x.VehiclePlate).HasColumnName("REC_PLACA_VEICULO");
            builder.Property(x => x.DriverName).HasColumnName("REC_NOME_MOTORISTA");
            builder.Property(x => x.DriverNumber).HasColumnName("REC_TELEFONE_MOTORISTA");
            builder.Property(x => x.OrderId).HasColumnName("PED_ID");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.Property(x => x.InvoiceTypeId).HasColumnName("TPF_ID");
            builder.Property(x => x.InstallationId).HasColumnName("INS_ID");
            builder.Property(x => x.ReceiverUser).HasColumnName("USU_ID_RECEBIMENTO_NOTAFISCAL_PEDIDO");
            builder.Property(x => x.ThirdPartyMaterial).HasColumnName("REC_MAT_TERCEIRO");

            builder.HasMany(x => x.ReceivementsMaterials).WithOne(x => x.ReceivementInvoiceOrder);
            builder.HasOne(x => x.Order).WithMany(x => x.ReceivementInvoiceOrders);
            builder.HasOne(x => x.Installation).WithMany(x => x.ReceivementInvoiceOrders);
            builder.HasOne(x => x.InvoiceType).WithMany(x => x.ReceivementInvoiceOrders);
            builder.HasOne(x => x.UserReceivementInvoiceOrder).WithMany();

            builder.UseUserModel();
        }
    }
}