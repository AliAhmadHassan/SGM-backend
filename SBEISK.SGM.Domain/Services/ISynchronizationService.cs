using System.Threading.Tasks;

namespace SBEISK.SGM.Domain.Services
{
    public interface ISynchronizationService
    {
        Task Execute(string syncName);
    }
}
