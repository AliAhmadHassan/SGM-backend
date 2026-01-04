using System;
using System.Runtime.Serialization;

namespace SBEISK.SGM.Domain.Exceptions
{
    [Serializable]
    public class EntityBadRequestException : Exception
    {
        protected EntityBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
           
        }
        public EntityBadRequestException(string message) : base(message)
        {
        }
    }
}
