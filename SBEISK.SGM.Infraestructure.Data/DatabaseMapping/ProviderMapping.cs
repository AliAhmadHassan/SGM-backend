using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("FORNECEDOR");
            builder.Property(x => x.Id).HasColumnName("FOR_ID");
            builder.Property(x => x.CompanyName).HasColumnName("FOR_RAZAO_SOCIAL");
            builder.Property(x => x.FantasyName).HasColumnName("FOR_NOME_FANTASIA");
            builder.Property(x => x.Telephone).HasColumnName("FOR_TELEFONE");
            builder.Property(x => x.Cnpj).HasColumnName("FOR_CNPJ");
            builder.Property(x => x.Street).HasColumnName("FOR_RUA");
            builder.Property(x => x.Number).HasColumnName("FOR_NUMERO");
            builder.Property(x => x.Complement).HasColumnName("FOR_COMPLEMENTO");
            builder.Property(x => x.Neighborhood).HasColumnName("FOR_BAIRRO");
            builder.Property(x => x.City).HasColumnName("FOR_CIDADE");
            builder.Property(x => x.Cep).HasColumnName("FOR_CEP");
            builder.Property(x => x.Uf).HasColumnName("FOR_UF");
            builder.Property(x => x.DeletedByProcedure).HasColumnName("FOR_DELECAO_PROCEDURE");

            builder.HasMany(x => x.ReceivementProviderReasons).WithOne(x => x.Provider);
            builder.HasMany(x => x.Orders).WithOne(x => x.Provider);
        }
    }
}