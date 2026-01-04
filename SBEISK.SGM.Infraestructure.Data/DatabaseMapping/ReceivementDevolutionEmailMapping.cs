using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ReceivementDevolutionEmailMapping : IEntityTypeConfiguration<ReceivementDevolutionEmail>
    {
        public void Configure(EntityTypeBuilder<ReceivementDevolutionEmail> builder)
        {
            builder.ToTable("REC_DEVOLUCAO_EMAIL");
            builder.Property(x => x.Id).HasColumnName("RDE_ID");
            builder.Property(x => x.Email).HasColumnName("RDE_EMAIL");
            builder.Property(x => x.ReceivementDevolutionReceiverId).HasColumnName("RDD_ID");

            builder.HasOne(x => x.ReceivementDevolutionReceiver).WithMany().HasForeignKey(x => x.ReceivementDevolutionReceiverId);
        }
    }
}