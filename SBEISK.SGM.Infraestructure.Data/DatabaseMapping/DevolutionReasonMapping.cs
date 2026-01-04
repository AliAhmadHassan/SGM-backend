using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DevolutionReasonMapping : IEntityTypeConfiguration<DevolutionReason>
    {
        public void Configure(EntityTypeBuilder<DevolutionReason> builder)
        {
            builder.ToTable("MOTIVO_DEVOLUCAO");
            builder.Property(x => x.Id).HasColumnName("MOD_ID");
            builder.Property(x => x.Description).HasColumnName("MOD_DESCRICAO");

            builder.HasMany(x => x.ReceivementDevolutionMaterials).WithOne(x => x.DevolutionReason);
        }
    }
}