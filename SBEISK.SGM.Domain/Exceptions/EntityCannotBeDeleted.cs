using System;
using System.Runtime.Serialization;

namespace SBEISK.SGM.Domain.Exceptions
{
    [Serializable]
    public class EntityCannotBeDeletedException : Exception
    {
        protected EntityCannotBeDeletedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
           
        }
        public EntityCannotBeDeletedException(string message) : base(message)
        {
        }
    }
}
