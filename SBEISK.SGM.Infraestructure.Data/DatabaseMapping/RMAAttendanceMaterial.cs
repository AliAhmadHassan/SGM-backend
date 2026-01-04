using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAAttendanceMaterialMapping : IEntityTypeConfiguration<RMAAttendanceMaterial>
    {
        public void Configure(EntityTypeBuilder<RMAAttendanceMaterial> builder)
        {
            
            builder.ToTable("RMA_ATEND_MATERIAL");
            builder.Property(x => x.Id).HasColumnName("RAM_ID");
            builder.Property(x => x.Quantity).HasColumnName("RAM_QUANTIDADE");
            builder.Property(x => x.RMAAttendanceID).HasColumnName("RMT_ID");
            builder.Property(x => x.MAterialId).HasColumnName("MAT_ID");
            builder.Property(x => x.RMAMaterialId).HasColumnName("RMM_ID"); 

            builder.HasOne(x => x.RMAAttendance).WithMany(x => x.Materials).HasForeignKey(x => x.RMAAttendanceID);
            builder.HasOne(x => x.RMAMaterial).WithMany(x => x.RMAAttendanceMaterial).HasForeignKey(x => x.RMAMaterialId);
        }
    }
}
