using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ExitPhotoBoardingMapping : IEntityTypeConfiguration<ExitPhotoBoarding>
    {
        public void Configure(EntityTypeBuilder<ExitPhotoBoarding> builder)
        {
            builder.ToTable("SAI_DIR_IMA_EMBARQUE");
            builder.Property(x => x.Id).HasColumnName("SIE_ID");
            builder.Property(x => x.Photo).HasColumnName("SIE_IMAGEM");
            builder.Property(x => x.DirectExitId).HasColumnName("SDD_ID");

            builder.HasOne(x => x.DirectExit).WithMany(x => x.Photos).HasForeignKey(x => x.DirectExitId);
        }
    }
}