using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class TransferRepository : Repository<Transfer>, ITransferRepository
    {
        public TransferRepository(SgmDataContext dataContext) : base(dataContext)
        {

        }

        public PaginatedQueryResult<Transfer> All(GenericPaginatedQuery<STMQuery> userQuery)
        {
            IQueryable<Transfer> stmsQueryable = ApplyFilter(userQuery.Filter);
            return ApplyPagination(stmsQueryable, userQuery);
        }

        public IQueryable<Transfer> ApplyFilter(STMQuery query)
        {
            IQueryable<Transfer> stmsQuery = DbSet.AsQueryable()
                                            .Include(s => s.STM)
                                                .ThenInclude(t => t.InstallationDestiny)
                                            .Include(s => s.STM)
                                                .ThenInclude(t => t.InstallationSource)
                                            .Include(s => s.STM)
                                                .ThenInclude(t => t.UserWithdraw)
                                            .Include(s => s.STM)
                                                .ThenInclude(t => t.UserRequester)
                                            .Include(s => s.TransferStatus)
                                            .Include(s => s.TransferMaterials);

            if (query == default(STMQuery))
                return stmsQuery;

            if (query.STM.HasValue)
                stmsQuery = stmsQuery.Where(s => s.STMId.Equals(query.STM));

            if (query.InitialDate.HasValue && query.FinishDate.HasValue)
                stmsQuery = stmsQuery.Where(s => s.PrevisionDate >= query.InitialDate && s.PrevisionDate <= query.FinishDate);

            if (query.RequiredUser.HasValue)
                stmsQuery = stmsQuery.Where(s => s.STM.UserIdRequester.Equals(query.RequiredUser));

            if (query.WithdrawUser.HasValue)
                stmsQuery = stmsQuery.Where(s => s.STM.UserIdWithdraw.Equals(query.WithdrawUser));

            if (query.InstallationSource.HasValue)
                stmsQuery = stmsQuery.Where(s => s.STM.InstallationSourceId.Equals(query.InstallationSource));

            if (query.InstallationDestiny.HasValue)
                stmsQuery = stmsQuery.Where(s => s.STM.InstallationDestinyId.Equals(query.InstallationDestiny));

            if (query.Transfer.HasValue)
                stmsQuery = stmsQuery.Where(s => s.TransferStatusId.Equals(query.Transfer));

            if (!string.IsNullOrEmpty(query.MaterialCode))
                stmsQuery = stmsQuery.Where(s => s.STM.STMMaterials.Select(m => m.Material.Code).Contains(query.MaterialCode));

            if (!string.IsNullOrEmpty(query.MaterialDescription))
                stmsQuery = stmsQuery.Where(s => s.STM.STMMaterials.Select(m => m.Material.Description).Contains(query.MaterialDescription));

            return stmsQuery;
        }
        public int LastIdTransfer()
        {
            Transfer transfer = DbSet.LastOrDefault();
            if (transfer != default(Transfer))
                return transfer.Id;
            return default(int);
        }

        public Transfer TransferById(int id)
        {
            Transfer transfer = Query(t => t.Id.Equals(id)).Include(t => t.TransferStatus)
                                              .Include(s => s.STM)
                                                .ThenInclude(u => u.UserRequester)
                                              .Include(s => s.STM)
                                                .ThenInclude(u => u.UserWithdraw)
                                              .Include(s => s.STM)
                                                .ThenInclude(u => u.InstallationSource)
                                              .Include(s => s.STM)
                                                .ThenInclude(u => u.InstallationDestiny)
                                              .FirstOrDefault();

            return transfer;
        }

        public void MergeTransfer(ICollection<Transfer> original, ICollection<Transfer> other, Action<Transfer, Transfer> updateStrategy)
        {
            Merger<Transfer> merger = new Merger<Transfer>((x, y) => x.Id == y.Id, (x, y) => x.UserId == y.UserId);
            MergeResult<Transfer> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.Transfers.RemoveRange(result.ItemsToDelete);
            this.DataContext.Transfers.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }
    }
}