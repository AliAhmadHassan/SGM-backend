using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class DisciplineMapping : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable("DISCIPLINA");
            builder.Property(x => x.Id).HasColumnName("DIS_ID");
            builder.Property(x => x.Description).HasColumnName("DIS_DESCRICAO");
        }
    }
}