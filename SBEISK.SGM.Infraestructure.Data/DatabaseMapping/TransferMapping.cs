using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class TransferMapping : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("TRANSFERENCIA");
            builder.Property(x => x.Id).HasColumnName("TRA_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("TRA_DT_CRIACAO");
            builder.Property(x => x.PrevisionDate).HasColumnName("TRA_DT_PREVISAO");
            builder.Property(x => x.FinishDate).HasColumnName("TRA_DT_CONCLUSAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("TRA_DT_ATUALIZACAO");
            builder.Property(x => x.VehiclePlate).HasColumnName("TRA_PLACA_VEICULO");
            builder.Property(x => x.DriverName).HasColumnName("TRA_NOME_MOTORISTA");
            builder.Property(x => x.DriverNumber).HasColumnName("TRA_TELEFONE_MOTORISTA");
            builder.Property(x => x.InvoiceNumber).HasColumnName("TRA_NR_DOC_FISCAL");
            builder.Property(x => x.InvoiceDate).HasColumnName("TRA_DT_DOC_FISCAL");
            builder.Property(x => x.Complement).HasColumnName("TRA_COMPLEMENTO");
            builder.Property(x => x.Notes).HasColumnName("TRA_OBSERVACAO");
            builder.Property(x => x.TransferStatusId).HasColumnName("TRS_ID");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.Property(x => x.UserWithdrawId).HasColumnName("USU_ID_RETIRADA");
            builder.Property(x => x.UserReceiverId).HasColumnName("USU_ID_RECEBIMENTO");
            builder.Property(x => x.STMId).HasColumnName("STM_ID");
            builder.Property(x => x.IsDraft).HasColumnName("TRA_RASCUNHO");

            builder.HasOne(x => x.STM).WithMany(x => x.Transfers).HasForeignKey(x => x.STMId);
            builder.HasOne(x => x.UserWithdraw).WithMany(x => x.TransferWithdraws).HasForeignKey(x => x.UserWithdrawId);
            builder.HasOne(x => x.UserReceiver).WithMany(x => x.TransferReceivements).HasForeignKey(x => x.UserReceiverId);
            builder.HasOne(x => x.TransferStatus).WithMany(x => x.Transfers).HasForeignKey(x => x.TransferStatusId);
            builder.HasMany(x => x.TransferMaterials).WithOne(x => x.Transfer);
            builder.HasMany(x => x.Emails).WithOne(x => x.Transfer);
            builder.HasMany(x => x.Attachments).WithOne(x => x.Transfer);
            builder.HasMany(x => x.Photos).WithOne(x => x.Transfer);
        }
    }
}