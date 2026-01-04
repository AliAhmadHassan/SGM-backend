using System;

namespace SBEISK.SGM.Domain.Entities.Base
{
    public interface ISoftDelete
    {
        DateTime? DeletedAt { get; set; }
    }
}
