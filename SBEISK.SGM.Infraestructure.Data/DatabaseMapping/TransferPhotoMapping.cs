using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class TransferPhotoMapping : IEntityTypeConfiguration<TransferPhoto>
    {
        public void Configure(EntityTypeBuilder<TransferPhoto> builder)
        {
            builder.ToTable("TRA_IMA_RECEBIMENTO");
            builder.Property(x => x.Id).HasColumnName("TIR_ID");
            builder.Property(x => x.Photo).HasColumnName("TIR_IMAGEM");
            builder.Property(x => x.TransferId).HasColumnName("TRA_ID");

            builder.HasOne(x => x.Transfer).WithMany(x => x.Photos).HasForeignKey(x => x.TransferId);
        }
    }
}