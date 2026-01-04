using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAAttendanceEmailMapping : IEntityTypeConfiguration<RMAAttendanceEmails>
    {
        public void Configure(EntityTypeBuilder<RMAAttendanceEmails> builder)
        {
            
            builder.ToTable("RMA_ATEND_EMAIL");
            builder.Property(x => x.Id).HasColumnName("RAE_ID");
            builder.Property(x => x.Email).HasColumnName("RAE_EMAIL");
            builder.Property(x => x.RMAAttendanceId).HasColumnName("RMT_ID");
        }
    }
}