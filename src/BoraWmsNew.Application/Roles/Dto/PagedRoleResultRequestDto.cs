using Abp.Application.Services.Dto;

namespace BoraWmsNew.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

