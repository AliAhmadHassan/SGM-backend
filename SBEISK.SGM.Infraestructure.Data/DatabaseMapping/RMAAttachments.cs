using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAAttachmentsMapping : IEntityTypeConfiguration<RMAattachments>
    {
        public void Configure(EntityTypeBuilder<RMAattachments> builder)
        {
            builder.ToTable("RMA_ANEXO");
            builder.Property(x => x.Id).HasColumnName("RMX_ID");
            builder.Property(x => x.File).HasColumnName("RMX_ANEXO");
            builder.Property(x => x.RMAId).HasColumnName("RMA_ID");

            builder.HasOne(x => x.RMA).WithMany(x => x.RMAattachments)
                .HasForeignKey(x => x.RMAId);
        }
    }
}