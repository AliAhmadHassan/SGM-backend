using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("ENDERECO");
            builder.Property(x => x.Id).HasColumnName("END_ID");
            builder.Property(x => x.Description).HasColumnName("END_DESCRICAO");
            builder.Property(x => x.PublicPlace).HasColumnName("END_LOGRADOURO");
            builder.Property(x => x.Number).HasColumnName("END_NUMERO");
            builder.Property(x => x.Neighborhood).HasColumnName("END_BAIRRO");
            builder.Property(x => x.Complement).HasColumnName("END_COMPLEMENTO");
            builder.Property(x => x.Reference).HasColumnName("END_REFERENCIA");
            builder.Property(x => x.UfId).HasColumnName("UF_ID");
            builder.Property(x => x.CityId).HasColumnName("CID_ID");
            builder.Property(x => x.Cep).HasColumnName("END_CEP");

            builder.HasOne(s => s.Uf).WithMany(g => g.Addresses);
            builder.HasOne(s => s.City).WithMany(g => g.Addresses);

            builder.UseTimestamped("END_DT_CRIACAO", "END_DT_ATUALIZACAO");
            builder.UseUserModel();
            builder.UseSoftDelete("END_DT_DELECAO");
        }
    }
}