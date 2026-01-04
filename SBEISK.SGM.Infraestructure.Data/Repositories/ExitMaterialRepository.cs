using System.Collections.Generic;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.ExitMaterial;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ExitMaterialRepository : Repository<ExitMaterial>, IExitMaterialRepository
    {
        private readonly IMaterialRepository materialRepository;
        public ExitMaterialRepository(SgmDataContext dataContext, IMaterialRepository materialRepository) : base(dataContext)
        {
            this.materialRepository = materialRepository;
        }
        public IEnumerable<ExitMaterial> AddExitMaterial(string json)
        {
            IList<ExitMaterialProjection> exitMaterials = JsonConvert.DeserializeObject<IList<ExitMaterialProjection>>(json);

            foreach (ExitMaterialProjection item in exitMaterials)
            {
                if (item.MaterialCode == string.Empty)
                {
                    continue;
                }

                Material material = this.materialRepository.MaterialByCode(item.MaterialCode);
                ExitMaterial exitMaterial = new ExitMaterial();
                exitMaterial.Amount = item.Amount;

                if(material != default(Material))
                {
                    exitMaterial.MaterialId = material.Id;
                }

                yield return exitMaterial;
            }
        }
    }
}