using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAAttendanceAttachmentsMapping : IEntityTypeConfiguration<RMAAttendanceAttachments>
    {
        public void Configure(EntityTypeBuilder<RMAAttendanceAttachments> builder)
        {
            
            builder.ToTable("RMA_ATEND_ANEXO");
            builder.Property(x => x.Id).HasColumnName("RMN_ID");
            builder.Property(x => x.Files).HasColumnName("RMN_ANEXO");
            builder.Property(x => x.RMAAttendanceId).HasColumnName("RMT_ID");
        }
    }
}