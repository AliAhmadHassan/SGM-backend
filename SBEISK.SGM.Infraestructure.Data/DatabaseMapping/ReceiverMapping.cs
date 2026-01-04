using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class ReceiverMapping : IEntityTypeConfiguration<Receiver>
    {
        public void Configure(EntityTypeBuilder<Receiver> builder)
        {
            builder.ToTable("DESTINATARIO");
            builder.Property(x => x.Id).HasColumnName("DES_ID");
            builder.Property(x => x.Description).HasColumnName("DES_DESCRICAO");
            builder.Property(x => x.Address).HasColumnName("DES_ENDERECO");
            builder.Property(x => x.ReceiverTypeId).HasColumnName("TPD_ID");
            builder.UseSoftDelete("DES_DATA_DELECAO");
            builder.UseTimestamped("DES_DATA_CRIACAO", "DES_DATA_ATUALIZACAO");
            builder.UseUserModel("USU_ID");
        }
    }
}