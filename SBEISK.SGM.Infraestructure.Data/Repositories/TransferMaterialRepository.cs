using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class TransferMaterialRepository : Repository<TransferMaterial>, ITransferMaterialRepository
    {
        private readonly ITransferRepository transferRepository;
        public TransferMaterialRepository(SgmDataContext dataContext, ITransferRepository transferRepository) : base(dataContext)
        {
            this.transferRepository = transferRepository;
        }
        public IEnumerable<TransferMaterial> AddTransferMaterial(int stmId)
        {   
            IList<STMMaterial> transferMaterials = this.DataContext.STMMaterials.Where(x => x.STMId.Equals(stmId)).ToList();

            for (int i = 0; i < transferMaterials.Count; i++)
            {
                TransferMaterial traMat = new TransferMaterial();
                traMat.Amount = transferMaterials[i].Amount;
                traMat.TransferId = this.transferRepository.LastIdTransfer();
                traMat.MaterialId = transferMaterials[i].MaterialId;
                traMat.STMMaterialId = transferMaterials[i].Id;

                yield return traMat;
            }
        }
    }
}