using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Queries;
using SBEISK.SGM.Domain.Repositories.Base;

namespace SBEISK.SGM.Domain.Repositories
{
    public interface ISTMRepository : IRepository<STM>
    {       
        int LastIdSTM(); 
        PaginatedQueryResult<STM> All(GenericPaginatedQuery<STMQuery> userQuery);
        STM STMById(int id);
    }
}