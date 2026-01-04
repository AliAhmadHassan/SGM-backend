using System;
using System.Runtime.Serialization;

namespace SBEISK.SGM.Domain.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
           
        }
        public EntityNotFoundException(string message = null) : base(message)
        {
        }
    }
}
