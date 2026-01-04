using System.Collections.Generic;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.ReceivementDevolutionMaterial;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementDevolutionMaterialRepository : Repository<ReceivementDevolutionMaterial>, IReceivementDevolutionMaterialRepository
    {
        private readonly IMaterialRepository materialRepository;
        public ReceivementDevolutionMaterialRepository(SgmDataContext dataContext, IMaterialRepository materialRepository) : base(dataContext)
        {
            this.materialRepository = materialRepository;            
        }

        public IEnumerable<ReceivementDevolutionMaterial> AddReceivementDevolutionMaterial(string json)
        {
            IList<ReceivementDevolutionMaterialProjection> devolutionMaterials = JsonConvert.DeserializeObject<IList<ReceivementDevolutionMaterialProjection>>(json);

            foreach (ReceivementDevolutionMaterialProjection devolution in devolutionMaterials)
            {
                Material material = this.materialRepository.MaterialByCode(devolution.MaterialCode);

                ReceivementDevolutionMaterial receivementDevolution = new ReceivementDevolutionMaterial();
                receivementDevolution.MaterialId = material.Id;
                receivementDevolution.Amount = devolution.Amount;
                receivementDevolution.AddtionalControl = devolution.AdditionalController;
                receivementDevolution.MaterialStatusId = devolution.MaterialStatusId;    
                receivementDevolution.DevolutionReasonId = devolution.DevolutionReasonId;

                yield return receivementDevolution;    
            }
        }
    }
}