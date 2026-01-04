using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class ReceiverTypeMapping : IEntityTypeConfiguration<ReceiverType>
    {
        public void Configure(EntityTypeBuilder<ReceiverType> builder)
        {
            builder.ToTable("TIP_DESTINATARIO");
            builder.Property(x => x.Id).HasColumnName("TPD_ID");
            builder.Property(x => x.Description).HasColumnName("TPD_DESCRICAO");
        }
    }
}