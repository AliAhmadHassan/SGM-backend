using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class TransferStatusMapping : IEntityTypeConfiguration<TransferStatus>
    {
        public void Configure(EntityTypeBuilder<TransferStatus> builder)
        {
            builder.ToTable("TRA_STATUS");
            builder.Property(x => x.Id).HasColumnName("TRS_ID");
            builder.Property(x => x.Description).HasColumnName("TRS_DESCRICAO");

            builder.HasMany(x => x.Transfers).WithOne(x => x.TransferStatus);
        }
    }
}