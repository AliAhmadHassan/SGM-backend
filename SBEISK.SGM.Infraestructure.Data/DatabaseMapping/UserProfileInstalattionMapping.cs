using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class UserProfileInstalattionMapping : IEntityTypeConfiguration<UserProfileInstallation>
    {
        public void Configure(EntityTypeBuilder<UserProfileInstallation> builder)
        {
            builder.ToTable("USUARIO_PERFIL_INSTALACAO");
            builder.Property(x => x.Id).HasColumnName("UPI_ID");
            builder.Property(x => x.UserId).HasColumnName("USU_ID");
            builder.Property(x => x.UserProfileId).HasColumnName("PER_ID");
            builder.Property(x => x.InstallationId).HasColumnName("INS_ID");
            builder.HasOne(x => x.User).WithMany(u => u.UserProfileInstallations);
        }
    }
}
