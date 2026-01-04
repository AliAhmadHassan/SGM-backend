using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Queries.TransferAttendanceMaterial;
using SBEISK.SGM.Domain.Repositories.Queries;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBEISK.SGM.Infraestructure.Data.Repositories.Queries
{
    public class VwTransferAttendanceMaterialReadOnlyRepository : ReadOnlyRepository<VwTransferAttendanceMaterial>, IVwTransferAttendanceMaterialReadOnlyRepository
    {
        private readonly SgmDataContext dataContext;
        public VwTransferAttendanceMaterialReadOnlyRepository(SgmDataContext dataContext) : base(dataContext)
        {
            this.dataContext = dataContext;
        }

        public PaginatedQueryResult<VwTransferAttendanceMaterial> All(GenericPaginatedQuery<VwTransferAttendanceMaterialQuery> transferAttendanceMaterialQuery)
        {
            IQueryable<VwTransferAttendanceMaterial> transferAttendanceMaterials = Filter(transferAttendanceMaterialQuery.Filter);
            return ApplyPagination(transferAttendanceMaterials, transferAttendanceMaterialQuery);
        }

        public IQueryable<VwTransferAttendanceMaterial> Filter(VwTransferAttendanceMaterialQuery query)
        {
            IQueryable<VwTransferAttendanceMaterial> queryable = base.Query();

            if (query == null)
                return queryable;

            if (query.STM.HasValue)
                queryable = queryable.Where(s => s.Stm_Id.Equals(query.STM));

            if (query.InitialDate.HasValue)
                queryable = queryable.Where(s => s.StmDtCriacao >= query.InitialDate && s.StmDtCriacao <= query.FinishDate);

            if (query.StatusStm.HasValue)
                queryable = queryable.Where(s => s.StsId.Equals(query.StatusStm));

            if (query.RequiredUser.HasValue)
                queryable = queryable.Where(s => s.UsuIdSolicitante.Equals(query.RequiredUser));

            if (query.WithdrawUser.HasValue)
                queryable = queryable.Where(s => s.UsuIdAprovador.Equals(query.WithdrawUser));

            if (query.InstallationSource.HasValue)
                queryable = queryable.Where(s => s.InsIdOrigem.Equals(query.InstallationSource));

            if (query.InstallationDestiny.HasValue)
                queryable = queryable.Where(s => s.InstIdDestino.Equals(query.InstallationDestiny));

            if (!string.IsNullOrEmpty(query.MaterialCode))
            {
                int materialId = DataContext.Materials.Where(x => x.Code.Contains(query.MaterialCode)).FirstOrDefault().Id;
                IList<STMMaterial> sTMMaterials = DataContext.STMMaterials.Where(item => item.MaterialId.Equals(materialId)).ToList();

                queryable = queryable.Where(item => sTMMaterials.Any(stmMaterial => stmMaterial.STMId.Equals(item.Stm_Id)));
            }

            if (!string.IsNullOrEmpty(query.MaterialDescription))
            {
                IQueryable<Material> materials = DataContext.Materials.Where(x => x.Description.Contains(query.MaterialDescription));
                IList<STMMaterial> sTMMaterials = DataContext.STMMaterials.Where(item => materials.Any(material => material.Id.Equals(item.MaterialId))).ToList();

                queryable = queryable.Where(item => sTMMaterials.Any(stmMaterial => stmMaterial.STMId.Equals(item.Stm_Id)));
            }
            return queryable;
        }

        public List<STMMaterial> GetMaterials(int id)
        {
            return this.DataContext.STMMaterials.Where(x => x.STMId.Equals(id))
                .Include(x => x.Material)
                .ToList();
        }

        public VwTransferAttendanceMaterial GetSTM(int id)
        {
            return this.DbQuery.FirstOrDefault(x => x.Stm_Id.Equals(id));
        }
    }
}
