using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class RMAStatusMapping : IEntityTypeConfiguration<RMAStatus>
    {
        public void Configure(EntityTypeBuilder<RMAStatus> builder)
        {
            builder.ToTable("RMA_STATUS");
            builder.Property(x => x.Id).HasColumnName("RMS_ID");
            builder.Property(x => x.Description).HasColumnName("RMS_DESCRICAO");
        }
    }
}