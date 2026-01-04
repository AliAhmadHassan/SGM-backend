using System.Collections.Generic;
using Newtonsoft.Json;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class STMMaterialRepository : Repository<STMMaterial>, ISTMMaterialRepository
    {
        private readonly IMaterialRepository materialRepository;
        private readonly ISTMRepository sTMRepository;
        public STMMaterialRepository(SgmDataContext dataContext, IMaterialRepository materialRepository, ISTMRepository sTMRepository) : base(dataContext)
        {            
            this.materialRepository = materialRepository;
            this.sTMRepository = sTMRepository;
        }

        public IEnumerable<STMMaterial> AddSTMMaterial(string request)
        {
            IList<STMMaterialProjection> materials = JsonConvert.DeserializeObject<IList<STMMaterialProjection>>(request);

            foreach (STMMaterialProjection item in materials)
            {
                if(item.MaterialCode == string.Empty)
                {
                    continue;
                }

                Material material = this.materialRepository.MaterialByCode(item.MaterialCode);

                STMMaterial sTMMaterial = new STMMaterial();
                sTMMaterial.Amount = item.Amount;
                sTMMaterial.MaterialId = material.Id;

                yield return sTMMaterial;
            }
        }
    }
}