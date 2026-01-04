using System;

namespace SBEISK.SGM.Domain.Entities.Base
{
    public interface ITimestampedModel
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
