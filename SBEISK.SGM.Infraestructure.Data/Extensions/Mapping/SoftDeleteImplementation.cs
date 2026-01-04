using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Infraestructure.Data.Extensions.Mapping
{
    public static class SoftDeleteImplementation
    {
        public const string SOFT_DELETE_DEFAULT_COLUMN_NAME = "DT_DELECAO";
        public static void UseSoftDelete<T>(this EntityTypeBuilder<T> builder, string columnName = SOFT_DELETE_DEFAULT_COLUMN_NAME) where T: class, ISoftDelete
        {
            builder.Property(x => x.DeletedAt).HasColumnName(columnName);
            builder.HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
        
        public static void UseSoftDelete<T>(this QueryTypeBuilder<T> builder, string columnName = SOFT_DELETE_DEFAULT_COLUMN_NAME) where T: class, ISoftDelete
        {
            builder.Property(x => x.DeletedAt).HasColumnName(columnName);
            builder.HasQueryFilter(x => !x.DeletedAt.HasValue);
        }
    }
}
