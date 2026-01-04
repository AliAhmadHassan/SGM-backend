using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Services;
using SBEISK.SGM.Infraestructure.Data.Context;

namespace SBEISK.SGM.Infraestructure.Data.Services
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly SgmDataContext context;

        public SynchronizationService(SgmDataContext context)
        {
            this.context = context;
        }
        public async Task Execute(string syncName)
        {
            Synchronization sync = await GetSyncByCommandNameAsync(syncName);
            ValidateSynchronization(sync, syncName);
            await context.Database.ExecuteSqlCommandAsync(sync.Command);
        }
        private async Task<Synchronization> GetSyncByCommandNameAsync(string syncName)
        {
            return await context.Synchronizations.FirstOrDefaultAsync(x => x.Target == syncName);
        }
        private void ValidateSynchronization(Synchronization sync, string syncName)
        {
            string message = $"Sync: {syncName}";
            if (sync == null)
            {
                throw new Exception($"{message} not found.");
            }
            if (sync.IsRunning)
            {
                throw new InvalidOperationException($"{message} is already running.");
            }
            if (string.IsNullOrEmpty(sync.Command))
            {
                throw new InvalidOperationException($"{message} not found.");
            }
        }
    }
}
