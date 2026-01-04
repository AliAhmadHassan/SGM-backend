using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Repositories.Base;


namespace SBEISK.SGM.Domain.Repositories.Queries
{
    public interface IRMADetailsReadOnlyRepository : IReadOnlyRepository<RMADetails>
    {
        RMADetails GetRMA(int id);
    }
}