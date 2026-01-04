using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USUARIO");
            builder.Property(u => u.Id).HasColumnName("USU_ID");
            builder.Property(u => u.Name).HasColumnName("USU_NOME");
            builder.Property(u => u.Active).HasColumnName("USU_ATIVO");
            builder.Property(u => u.Email).HasColumnName("USU_EMAIL");
            builder.Property(u => u.DeletedAt).HasColumnName("USU_DT_DELECAO");

            builder.HasMany(u => u.UserProfileInstallations).WithOne(u => u.User);
            builder.HasMany(u => u.Projects).WithOne(u => u.User);
            builder.HasMany(u => u.Receivers).WithOne(u => u.User);
            builder.HasMany(u => u.STMWithdraws).WithOne(u => u.UserWithdraw);
            builder.HasMany(u => u.STMRequesters).WithOne(u => u.UserRequester);
            builder.HasMany(u => u.TransferReceivements).WithOne(u => u.UserReceiver);
            builder.HasMany(u => u.TransferWithdraws).WithOne(u => u.UserWithdraw);

            builder.UseSoftDelete("USU_DT_DELECAO");
        }
    }
}