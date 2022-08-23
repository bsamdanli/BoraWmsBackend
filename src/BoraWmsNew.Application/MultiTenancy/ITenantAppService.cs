using Abp.Application.Services;
using BoraWmsNew.MultiTenancy.Dto;

namespace BoraWmsNew.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

