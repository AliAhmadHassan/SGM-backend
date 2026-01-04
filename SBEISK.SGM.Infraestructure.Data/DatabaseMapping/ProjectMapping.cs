using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ProjectMapping : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("PROJETO");
            builder.Property(x => x.Id).HasColumnName("PRO_ID");
            builder.Property(x => x.Id).HasColumnName("PRO_ID");
            builder.Property(x => x.Description).HasColumnName("PRO_DESCRICAO");
            builder.Property(x => x.Initials).HasColumnName("PRO_SIGLA");
            builder.Property(x => x.Active).HasColumnName("PRO_ATIVO");
            builder.Property(x => x.BranchOfficeId).HasColumnName("FIL_ID");
            builder.Property(x => x.CreatedAt).HasColumnName("PRO_DT_CRIACAO");
            builder.Property(x => x.UpdatedAt).HasColumnName("PRO_DT_ATUALIZACAO");
            builder.Property(x => x.DeletedAt).HasColumnName("PRO_DT_DELECAO");
            builder
                .HasOne<User>(s => s.User)
                .WithMany(g => g.Projects)
                .HasForeignKey(s => s.UserId); 
            builder
                .HasOne<BranchOffice>(s => s.BranchOffice)
                .WithMany(g => g.Projects)
                .HasForeignKey(s => s.BranchOfficeId);
            builder.UseUserModel();

            builder.UseTimestamped("PRO_DT_CRIACAO", "PRO_DT_ATUALIZACAO");
            builder.UseUserModel();
            builder.UseSoftDelete("PRO_DT_DELECAO");
        }
    }
}
