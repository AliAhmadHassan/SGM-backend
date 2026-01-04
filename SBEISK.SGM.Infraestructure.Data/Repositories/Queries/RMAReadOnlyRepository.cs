using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;


namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class RMAReadOnlyRepository : ReadOnlyRepository<RMA>, IRMAReadOnlyRepository
    {
        private readonly SgmDataContext dataContext;
        public RMAReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public RMA GetRMA(int id)
        {
            return this.DbQuery.FirstOrDefault(x => x.RMAId == id);
        }

        public List<RMAMaterial> GetMaterials(int id)
        {
            return this.DataContext.RMAMaterials.Where(x => x.RMAId == id).Include(x => x.Material).Include(x => x.Discipline).ToList();
        }

        public PaginatedQueryResult<RMA> All(GenericPaginatedQuery<RMAQuery> RMAQuery)
        {
            IQueryable<RMA> RMAs = Filter(RMAQuery.Filter);

            return ApplyPagination(RMAs, RMAQuery);
        }

        public IQueryable<RMA> Filter(RMAQuery query)
        {
            IQueryable<RMA> queryable = base.Query();

            if(query == null)
                return queryable;
            
            if (query.RMA.HasValue)
                queryable = queryable.Where(s => s.RMAId.Equals(query.RMA));

            if (query.InitialDate.HasValue && query.FinishDate.HasValue)
                queryable = queryable.Where(s => s.EmissionDate >= query.InitialDate && s.EmissionDate.Date <= query.FinishDate);

            if (!string.IsNullOrEmpty(query.User))
                queryable = queryable.Where(s => s.ApproverUser.Equals(query.User));

            if (!string.IsNullOrEmpty(query.InstallationSource))
                queryable = queryable.Where(s => s.Installation.Equals(query.InstallationSource));

            if (query.ReceiverCode.HasValue)
                queryable = queryable.Where(s => s.ReceiverCode.Equals(query.ReceiverCode));

             if (!string.IsNullOrEmpty(query.ReceiverName))
                queryable = queryable.Where(s => s.ReceiverName.Contains(query.ReceiverName));

            if (!string.IsNullOrEmpty(query.Status))
                queryable = queryable.Where(s => s.Status.Equals(query.Status));
            
           if(!string.IsNullOrEmpty(query.MaterialCode))
            {
                int materialId = DataContext.Materials.Where(x => x.Code.Contains(query.MaterialCode)).FirstOrDefault().Id;
                IList<RMAMaterial> RMAMaterials = DataContext.RMAMaterials.Where(x => x.MaterialId.Equals(materialId)).ToList();
                queryable = queryable.Where(item => RMAMaterials.Any(RMAItem => RMAItem.RMAId == item.RMAId));  
            }                
            
            if(!string.IsNullOrEmpty(query.MaterialDescription))
            {        
                IQueryable<Material> materials = DataContext.Materials.Where(x => x.Description.Contains(query.MaterialDescription));
                IList<RMAMaterial> RMAMaterials = DataContext.RMAMaterials.Where(item => materials.Any(material => material.Id.Equals(item.MaterialId))).ToList();
                queryable = queryable.Where(item => RMAMaterials.Any(RMAMaterial => RMAMaterial.RMAId == item.RMAId));    
            }                

            return queryable;
        }
    }
}