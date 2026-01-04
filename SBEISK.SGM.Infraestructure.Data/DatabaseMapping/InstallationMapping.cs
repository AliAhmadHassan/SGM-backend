using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class InstallationMapping : IEntityTypeConfiguration<Installation>
    {
        public void Configure(EntityTypeBuilder<Installation> builder)
        {
            builder.ToTable("INSTALACAO");
            builder.Property(x => x.Id).HasColumnName("INS_ID");
            builder.Property(x => x.Name).HasColumnName("INS_NOME");
            builder.Property(x => x.Description).HasColumnName("INS_DESCRICAO");
            builder.Property(x => x.ThirdMaterial).HasColumnName("INS_MATERIAL_TERCEIRO");
            builder.Property(x => x.AddressId).HasColumnName("END_ID");
            builder.Property(x => x.TypeId).HasColumnName("TPI_ID");
            builder.Property(x => x.ProjectId).HasColumnName("PRO_ID");
            builder
                .HasOne(x => x.Address)
                .WithMany(x => x.Installations)
                .HasForeignKey(x => x.AddressId);
            builder
                .HasOne(x => x.InstallationType)
                .WithMany(x => x.Installations)
                .HasForeignKey(x => x.TypeId);
            builder
                .HasOne(x => x.Project)
                .WithMany(x => x.Installations)
                .HasForeignKey(x => x.ProjectId);
            builder
                .HasMany(x => x.STMsSources)
                .WithOne(x => x.InstallationSource);
            builder
                .HasMany(x => x.STMsDestiny)
                .WithOne(x => x.InstallationDestiny);            

            builder.UseSoftDelete("INS_DT_DELECAO");
            
            builder.UseTimestamped("INS_DT_CRIACAO","INS_DT_ATUALIZACAO");
            builder.UseUserModel();
        }
    }
}
