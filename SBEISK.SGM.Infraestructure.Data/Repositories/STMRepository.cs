using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class STMRepository : Repository<STM>, ISTMRepository
    {
        public STMRepository(SgmDataContext dataContext) : base(dataContext)
        {
        }

        public PaginatedQueryResult<STM> All(GenericPaginatedQuery<STMQuery> userQuery)
        {
            IQueryable<STM> stmsQueryable = ApplyFilter(userQuery.Filter);
            return ApplyPagination(stmsQueryable, userQuery);
        }

        public IQueryable<STM> ApplyFilter(STMQuery query)
        {
            IQueryable<STM> stmsQuery = DbSet.AsQueryable()
                                        .Include(s => s.UserWithdraw)
                                        .Include(s => s.UserRequester)
                                        .Include(s => s.InstallationDestiny)
                                        .Include(s => s.InstallationSource)
                                        .Include(s => s.SolicitationStatus)
                                        .Include(s => s.STMMaterials)
                                        .Include(s => s.Transfers)
                                            .ThenInclude(t => t.TransferStatus);

            if (query == default(STMQuery))
                return stmsQuery;

            if (query.STM.HasValue)
                stmsQuery = stmsQuery.Where(s => s.Id.Equals(query.STM));

            if (query.InitialDate.HasValue && query.FinishDate.HasValue)
                stmsQuery = stmsQuery.Where(s => s.EmissionDate >= query.InitialDate && s.EmissionDate <= query.FinishDate);

            if (query.RequiredUser.HasValue)
                stmsQuery = stmsQuery.Where(s => s.UserIdRequester.Equals(query.RequiredUser));

            if (query.WithdrawUser.HasValue)
                stmsQuery = stmsQuery.Where(s => s.UserIdWithdraw.Equals(query.WithdrawUser));

            if (query.InstallationSource.HasValue)
                stmsQuery = stmsQuery.Where(s => s.InstallationSourceId.Equals(query.InstallationSource));

            if (query.InstallationDestiny.HasValue)
                stmsQuery = stmsQuery.Where(s => s.InstallationDestinyId.Equals(query.InstallationDestiny));

            if (query.Transfer.HasValue)
                stmsQuery = stmsQuery.Where(s => s.Transfers.Select(x => x.Id).Equals(query.Transfer));

            if (!string.IsNullOrEmpty(query.MaterialCode))
                stmsQuery = stmsQuery.Where(s => s.STMMaterials.Select(m => m.Material.Code).Contains(query.MaterialCode));

            if (!string.IsNullOrEmpty(query.MaterialDescription))
                stmsQuery = stmsQuery.Where(s => s.STMMaterials.Select(m => m.Material.Description).Contains(query.MaterialDescription));

            return stmsQuery;
        }

        public int LastIdSTM()
        {
            STM last = DbSet.LastOrDefault();
            if (last != null)
                return last.Id;
            return default(int);
        }

        public STM STMById(int id)
        {
            STM stm = Query(t => t.Id.Equals(id)).Include(s => s.InstallationSource)
                                                    .ThenInclude(i => i.Project.BranchOffice)
                                                 .Include(s => s.InstallationDestiny)
                                                    .ThenInclude(i => i.Project.BranchOffice)
                                                 .Include(s => s.UserWithdraw)
                                                 .Include(s => s.UserRequester)
                                                 .Include(s => s.SolicitationStatus)
                                                 .Include(s => s.Transfers)
                                                    .ThenInclude(t => t.STM)
                                              .FirstOrDefault();

            return stm;
        }
    }
}