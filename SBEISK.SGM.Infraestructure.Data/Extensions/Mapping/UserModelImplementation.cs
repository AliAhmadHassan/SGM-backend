using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Infraestructure.Data.Extensions.Mapping
{
    public static class UserModelImplementation
    {
        public const string USER_ID_COLUMN_NAME = "USU_ID_CRIACAO";
        public static void UseUserModel<T>(this EntityTypeBuilder<T> builder, string columnName = USER_ID_COLUMN_NAME)
            where T: class, IUserModel
        {
            builder.Property(x => x.UserId).HasColumnName(columnName);
        }
    }
}
