using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAAttendanceMapping : IEntityTypeConfiguration<RMAAttendance>
    {
        public void Configure(EntityTypeBuilder<RMAAttendance> builder)
        {
            builder.ToTable("RMA_ATENDIMENTO");
            builder.Property(x => x.Id).HasColumnName("RMT_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("RMT_DT_CRIACAO");
            builder.Property(x => x.IsDraft).HasColumnName("RMT_RASCUNHO");
            builder.Property(x => x.UpdatedAt).HasColumnName("RTM_DT_ATUALIZACAO");
            builder.Property(x => x.UserId).HasColumnName("USU_ID_CRIACAO");
            builder.Property(x => x.DeliverAt).HasColumnName("RMT_DT_ENTREGA");
            builder.Property(x => x.Comments).HasColumnName("RMT_OBSERVACAO");
            builder.Property(x => x.DeliverUserId).HasColumnName("USU_ID_ENTREGA");
            builder.Property(x => x.ReceiverUserId).HasColumnName("USU_ID_RETIRADA");
            
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.DeliverUser).WithMany().HasForeignKey(x => x.DeliverUserId);
            builder.HasOne(x => x.ReceiverUser).WithMany().HasForeignKey(x => x.ReceiverUserId);
            builder.HasMany(x => x.Attachments).WithOne(x => x.RMAattendance);
            builder.HasMany(x => x.Emails).WithOne(x => x.RMAAttendance);
        }
    }
}