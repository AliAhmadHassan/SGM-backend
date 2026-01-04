using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ExitReceiverEmailMapping : IEntityTypeConfiguration<ExitEmail>
    {
        public void Configure(EntityTypeBuilder<ExitEmail> builder)
        {
            builder.ToTable("SAI_DIR_EMAIL");
            builder.Property(x => x.Id).HasColumnName("SDE_ID");
            builder.Property(x => x.DirectExitId).HasColumnName("SDD_ID");
            builder.Property(x => x.Email).HasColumnName("SDE_EMAIL");

            builder.HasOne(x => x.DirectExit).WithMany(x => x.Emails).HasForeignKey(x => x.DirectExitId);
        }
    }
}