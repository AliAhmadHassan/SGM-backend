using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Infraestructure.Data.Extensions.Mapping;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class BranchOfficeMapping : IEntityTypeConfiguration<BranchOffice>
    {
        public void Configure(EntityTypeBuilder<BranchOffice> builder)
        {
            builder.ToTable("FILIAL");
            builder.Property(x => x.Id).HasColumnName("FIL_ID");
            builder.Property(x => x.Cnpj).HasColumnName("FIL_CNPJ");
            builder.Property(x => x.Description).HasColumnName("FIL_DESCRICAO");
            builder.Property(x => x.Street).HasColumnName("FIL_RUA");
            builder.Property(x => x.Number).HasColumnName("FIL_NUMERO");
            builder.Property(x => x.Complement).HasColumnName("FIL_COMPLEMENTO");
            builder.Property(x => x.Neighborhood).HasColumnName("FIL_BAIRRO");
            builder.Property(x => x.City).HasColumnName("FIL_CIDADE");
            builder.Property(x => x.Cep).HasColumnName("FIL_CEP");
            builder.Property(x => x.Uf).HasColumnName("FIL_UF");
            builder.Property(x => x.FantasyName).HasColumnName("FIL_NOME_FANTASIA");
            builder.Property(x => x.DeletedByProcedure).HasColumnName("FIL_DELECAO_PROCEDURE");
            //builder.Property(x => x.DeletedAt).HasColumnName("FIL_DATA_DELECAO");

            //builder.UseSoftDelete("FIL_DATA_DELECAO");
        }
    }
}
