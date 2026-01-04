using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.RMA;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class RMARepository : Repository<RequisitionOfMaterialForApplication>, IRMARepository
    {
        private readonly SgmDataContext dataContext;

        public RMARepository(SgmDataContext dataContext) : base(dataContext)
        {            
            this.dataContext = dataContext;
        }      

        public IList<RMAMaterial> RMAMaterials(int id, List<decimal> amounts)
        {
            List<RMAMaterial> queryable = this.dataContext.RMAMaterials.Where(u => u.RMAId == id).Include(x => x.Material).Include(x => x.Discipline).ToList();
            
            for (int i = 0; i < amounts.Count; i++)
            {
                queryable[i].AmountReceived += amounts[i];
            }
            
            return queryable;
        }

        public IEnumerable<RMAMaterial> NewMaterials(List<RMAMaterialProjection> materialsRequest)
        {
            foreach (RMAMaterialProjection mat in materialsRequest)
            {
                RMAMaterial material = new RMAMaterial();
                material.DisciplineId = mat.DisciplineId;
                material.MaterialId = this.DataContext.Materials.FirstOrDefault(x => x.Code == mat.MaterialCode).Id;
                material.Quantity = mat.AmountReceived;
                yield return material;
            }
            yield break;
        }             
    }
}
