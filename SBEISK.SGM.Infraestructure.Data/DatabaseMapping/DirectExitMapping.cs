using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ExitReceiverMapping : IEntityTypeConfiguration<DirectExit>
    {
        public void Configure(EntityTypeBuilder<DirectExit> builder)
        {
            builder.ToTable("SAIDA_DIRETA");
            builder.Property(x => x.Id).HasColumnName("SDD_ID");
            builder.Property(x => x.VehiclePlate).HasColumnName("SDD_PLACA_VEICULO");
            builder.Property(x => x.DriverName).HasColumnName("SDD_NOME_MOTORISTA");
            builder.Property(x => x.DriverNumber).HasColumnName("SDD_TELEFONE_MOTORISTA");
            builder.Property(x => x.Notes).HasColumnName("SDD_OBSERVACAO");
            builder.Property(x => x.CompanyNameThirdy).HasColumnName("SDD_RAZAO_SOCIAL_TERCEIRO");
            builder.Property(x => x.CNPJThirdy).HasColumnName("SDD_CNPJ_TERCEIRO");
            builder.Property(x => x.PrevisionDate).HasColumnName("SDD_DT_PREVISAO");
            builder.Property(x => x.DepartureDate).HasColumnName("SDD_DT_EMBARQUE");
            builder.Property(x => x.DeliveryDate).HasColumnName("SDD_DT_ENTREGA");
            builder.Property(x => x.ReferenceLocalDelivery).HasColumnName("REFERENCIA_LOCAL_ENTREGA");
            builder.Property(x => x.ContactDelivery).HasColumnName("CONTATO_ENTREGA");
            builder.Property(x => x.TelephoneDelivery).HasColumnName("TELEFONE_ENTREGA");
            builder.Property(x => x.ReceiverId).HasColumnName("DES_ID");
            builder.Property(x => x.UserDeliveryId).HasColumnName("USU_ID_ENTREGA");
            builder.Property(x => x.UserWithdrawId).HasColumnName("USU_ID_RETIRADA");
            builder.Property(x => x.InstallationDestinyId).HasColumnName("INS_ID_DESTINO");
            builder.Property(x => x.InstallationSourceId).HasColumnName("INS_ID_ORIGEM");
            builder.Property(x => x.ReceiverTypeId).HasColumnName("TPD_ID");
            builder.Property(x => x.ExitStatusId).HasColumnName("SDS_ID");
            builder.Property(x => x.ReasonProvisionGuideId).HasColumnName("MGP_ID");
            builder.Property(x => x.ExitTypeId).HasColumnName("TSD_ID");
            builder.Property(x => x.IsDraft).HasColumnName("SDD_RASCUNHO");

            builder.HasOne(x => x.UserWithdraw).WithMany().HasForeignKey(x => x.UserWithdrawId);
            builder.HasOne(x => x.UserDelivery).WithMany().HasForeignKey(x => x.UserDeliveryId);
            builder.HasOne(x => x.InstallationDestiny).WithMany().HasForeignKey(x => x.InstallationDestinyId);
            builder.HasOne(x => x.IntallationSource).WithMany().HasForeignKey(x => x.InstallationSourceId);
            builder.HasOne(x => x.Receiver).WithMany().HasForeignKey(x => x.ReceiverId);
            builder.HasOne(x => x.ReceiverType).WithMany().HasForeignKey(x => x.ReceiverTypeId);
            builder.HasOne(x => x.ExitType).WithMany().HasForeignKey(x => x.ExitTypeId);
            builder.HasOne(x => x.ExitStatus).WithMany().HasForeignKey(x => x.ExitStatusId);
            builder.HasOne(x => x.ReasonProvisionGuide).WithMany().HasForeignKey(x => x.ReasonProvisionGuideId);
            builder.HasMany(x => x.Attachments).WithOne(x => x.DirectExit);
            builder.HasMany(x => x.Emails).WithOne(x => x.DirectExit);
            builder.HasMany(x => x.ExitMaterials).WithOne(x => x.DirectExit);
        }
    }
}