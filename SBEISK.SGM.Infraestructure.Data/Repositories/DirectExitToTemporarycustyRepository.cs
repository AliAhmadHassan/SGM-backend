using System;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Exceptions;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class DirectExitToTemporarycustyRepository : Repository<DirectExitReceiver>, IDirectExitToTemporarycustyRepository
    {
        private readonly SgmDataContext dataContext;
        private readonly int ReceiverTypeEmployee = 3;
        public DirectExitToTemporarycustyRepository(SgmDataContext dataContext) : base(dataContext)
        {            
           this.dataContext = dataContext;
        }

       public override DirectExitReceiver Add(DirectExitReceiver entity)
        {
            Receiver receiver = dataContext.Receiver.Find(entity.ReceiverId);
            if(receiver.ReceiverTypeId != ReceiverTypeEmployee)
            {
                throw new UnauthorizedAccessException("O destinatário não é uma pessoa física");
            }
            
            return base.Add(entity);
        }
    }
}