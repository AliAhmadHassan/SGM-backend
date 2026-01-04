using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Infraestructure.Data.Extensions.Mapping
{
    public static class TimestampedImplementation
    {
        public const string CREATED_AT_COLUMN_NAME = "DT_CRIACAO";
        public const string UPDATED_AT_COLUMN_NAME = "DT_ATUALIZACAO";
        public static void UseTimestamped<T>(this EntityTypeBuilder<T> builder, string createdAtColumnName = CREATED_AT_COLUMN_NAME, string updatedAtColumnName = UPDATED_AT_COLUMN_NAME)
            where T: class, ITimestampedModel
        {
            builder.Property(x => x.CreatedAt).HasColumnName(createdAtColumnName);
            builder.Property(x => x.UpdatedAt).HasColumnName(updatedAtColumnName);
        }
    }
}
