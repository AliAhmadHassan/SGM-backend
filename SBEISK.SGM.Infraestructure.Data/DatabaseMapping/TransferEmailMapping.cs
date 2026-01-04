using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class TransferEmailMapping : IEntityTypeConfiguration<TransferEmail>
    {
        public void Configure(EntityTypeBuilder<TransferEmail> builder)
        {
            builder.ToTable("TRA_EMAIL");
            builder.Property(x => x.Id).HasColumnName("TRE_ID");
            builder.Property(x => x.UserId).HasColumnName("USU_ID");
            builder.Property(x => x.TransferId).HasColumnName("TRA_ID");

            builder.HasOne(x => x.Transfer).WithMany(x => x.Emails).HasForeignKey(x => x.TransferId);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}