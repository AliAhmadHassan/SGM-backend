using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ActionMapping : IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.ToTable("ACAO");
            builder.Property(x => x.Id).HasColumnName("ACA_ID");
            builder.Property(x => x.Description).HasColumnName("ACA_DESCRICAO");
            builder.Property(x => x.ParentActionId).HasColumnName("ACA_ID_PAI");
            builder.HasOne(x => x.ParentAction).WithMany(x => x.ChildrenActions);
        }
    }
}
