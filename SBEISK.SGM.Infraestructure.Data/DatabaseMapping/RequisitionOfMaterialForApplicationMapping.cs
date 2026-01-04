using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAMapping : IEntityTypeConfiguration<RequisitionOfMaterialForApplication>
    {
        public void Configure(EntityTypeBuilder<RequisitionOfMaterialForApplication> builder)
        {
            builder.ToTable("RMA");
            builder.Property(x => x.Id).HasColumnName("RMA_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("RMA_DT_CRIACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("RMA_DT_ATUALIZACAO");
            builder.Property(x => x.ApprovementDate).HasColumnName("RMA_DT_APROVACAO");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.Property(x => x.Comments).HasColumnName("RMA_OBSERVACAO");
            builder.Property(x => x.ApproverUserId).HasColumnName("USU_ID_APROVADOR");
            builder.Property(x => x.ReceiverUserId).HasColumnName("USU_ID_RETIRADA");
            builder.Property(x => x.ReceiverId).HasColumnName("DES_ID");
            builder.Property(x => x.StatusId).HasColumnName("RMS_ID");
            builder.Property(x => x.InstallationId).HasColumnName("INS_ID");
            builder.HasOne(x => x.Installation).WithMany(x => x.RMAs);

            builder.HasOne(x => x.Status).WithOne(x => x.RMA);
            builder.HasMany(x => x.Materials);
            builder.HasOne(x => x.ApproverUser).WithMany().HasForeignKey(x => x.ApproverUserId);
            builder.HasOne(x => x.Receiver).WithMany(x => x.RMA).HasForeignKey(x => x.ReceiverId);
            builder.UseUserModel();
        }
    }
}