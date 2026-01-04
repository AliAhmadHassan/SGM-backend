using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ExitStatusMapping : IEntityTypeConfiguration<ExitStatus>
    {
        public void Configure(EntityTypeBuilder<ExitStatus> builder)
        {
            builder.ToTable("SAI_DIR_STATUS");
            builder.Property(x => x.Id).HasColumnName("SDS_ID");
            builder.Property(x => x.Description).HasColumnName("SDS_DESCRICAO");

            builder.HasMany(x => x.DirectExit).WithOne(x => x.ExitStatus);
        }
    }
}