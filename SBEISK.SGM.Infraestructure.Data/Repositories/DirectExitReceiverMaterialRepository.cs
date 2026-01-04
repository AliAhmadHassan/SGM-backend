using System.Collections.Generic;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.DirectExitReceiver;
using SBEISK.SGM.Domain.Projections.TemporaryCustodyMaterial;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class DirectExitReceiverMaterialRepository : Repository<DirectExitReceiverMaterial>, IDirectExitReceiverMaterialRepository
    {
        private readonly IDirectExitReceiverRepository exitReceiverRepository;
        private readonly IMaterialRepository materialRepository;
        public DirectExitReceiverMaterialRepository(SgmDataContext dataContext, IDirectExitReceiverRepository exitReceiverRepository,
                                                    IMaterialRepository materialRepository) : base(dataContext)
        {
            this.exitReceiverRepository = exitReceiverRepository;
            this.materialRepository = materialRepository;
        }

        public IEnumerable<DirectExitReceiverMaterial> AddExitReceiverMaterial(string json)
        {
            IList<DirectExitReceiverMaterialProjection> exitMaterials = JsonConvert.DeserializeObject<IList<DirectExitReceiverMaterialProjection>>(json);

            foreach (DirectExitReceiverMaterialProjection item in exitMaterials)
            {
                if(item.MaterialCode == string.Empty)
                {
                    continue;
                }

                Material material = this.materialRepository.MaterialByCode(item.MaterialCode);

                DirectExitReceiverMaterial directExitReceiver = new DirectExitReceiverMaterial();
                directExitReceiver.MaterialId = material.Id;
                directExitReceiver.Amount = item.Amount;
                directExitReceiver.DisciplineId = item.DisciplineId;
               

                yield return directExitReceiver;
            }
        }

        public IEnumerable<DirectExitReceiverMaterial> AddExitTemporaryCustodyMaterial(string json)
        {
            IList<TemporaryCustodyMaterialProjection> exitMaterials = JsonConvert.DeserializeObject<IList<TemporaryCustodyMaterialProjection>>(json);

            foreach (TemporaryCustodyMaterialProjection item in exitMaterials)
            {
                Material material = this.materialRepository.MaterialByCode(item.MaterialCode);

                DirectExitReceiverMaterial directExitReceiver = new DirectExitReceiverMaterial();
                directExitReceiver.MaterialId = material.Id;
                directExitReceiver.Amount = item.Amount;
                directExitReceiver.ReasonOfTemporaryCustodyId = item.ReasonOfTemporaryCustodyId;
               

                yield return directExitReceiver;
            }
        }
    }
}