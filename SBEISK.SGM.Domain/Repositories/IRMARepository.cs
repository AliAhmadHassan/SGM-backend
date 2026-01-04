using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.RMA;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.Provider;
using SBEISK.SGM.Domain.Repositories.Base;

namespace  SBEISK.SGM.Domain.Repositories
{
    public interface IRMARepository : IRepository<RequisitionOfMaterialForApplication>
    {
        IList<RMAMaterial> RMAMaterials(int id, List<decimal> amounts);
        IEnumerable<RMAMaterial> NewMaterials(List<RMAMaterialProjection> materialsRequest);
    }
}