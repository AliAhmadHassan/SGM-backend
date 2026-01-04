using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementPhotoMapping : IEntityTypeConfiguration<ReceivementPhoto>
    {
        public void Configure(EntityTypeBuilder<ReceivementPhoto> builder)
        {
            builder.ToTable("REC_IMAGEM");
            builder.Property(x => x.Id).HasColumnName("RCI_ID");
            builder.Property(x => x.Photo).HasColumnName("RCI_IMAGEM");
            builder.Property(x => x.ReceivementInvoiceOrderId).HasColumnName("REC_ID");

            builder.HasOne(x => x.ReceivementInvoiceOrder).WithMany(x => x.ReceivementPhotos)
                .HasForeignKey(x => x.ReceivementInvoiceOrderId);
        }
    }
}