using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Receiving.Dto
{
    public class PagedDocumentResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
