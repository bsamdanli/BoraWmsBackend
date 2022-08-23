using System.Threading.Tasks;
using Abp.Application.Services;
using BoraWmsNew.Sessions.Dto;

namespace BoraWmsNew.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
