using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class STMMapping : IEntityTypeConfiguration<STM>
    {
        public void Configure(EntityTypeBuilder<STM> builder)
        {
            builder.ToTable("STM");
            builder.Property(x => x.Id).HasColumnName("STM_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("STM_DT_CRIACAO");
            builder.Property(x => x.EmissionDate).HasColumnName("STM_DT_APROVACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("STM_DT_ATUALIZACAO");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.Property(x => x.UserIdWithdraw).HasColumnName("USU_ID_APROVADOR");
            builder.Property(x => x.UserIdRequester).HasColumnName("USU_ID_SOLICITANTE");
            builder.Property(x => x.InstallationSourceId).HasColumnName("INS_ID_ORIGEM");
            builder.Property(x => x.InstallationDestinyId).HasColumnName("INS_ID_DESTINO");
            builder.Property(x => x.SolicitationStatusId).HasColumnName("STS_ID");
            builder.Property(x => x.IsDraft).HasColumnName("STM_RASCUNHO");

            builder.HasOne(x => x.UserWithdraw).WithMany(x => x.STMWithdraws).HasForeignKey(x => x.UserIdWithdraw);
            builder.HasOne(x => x.UserRequester).WithMany(x => x.STMRequesters).HasForeignKey(x => x.UserIdRequester);
            builder.HasOne(x => x.InstallationDestiny).WithMany(x => x.STMsDestiny).HasForeignKey(x => x.InstallationDestinyId);
            builder.HasOne(x => x.InstallationSource).WithMany(x => x.STMsSources).HasForeignKey(x => x.InstallationSourceId);
            builder.HasOne(x => x.SolicitationStatus).WithMany(x => x.STMs).HasForeignKey(x => x.SolicitationStatusId);
            builder.HasMany(x => x.Attachments).WithOne(x => x.STM);
            builder.HasMany(x => x.Emails).WithOne(x => x.STM);
        }
    }
}