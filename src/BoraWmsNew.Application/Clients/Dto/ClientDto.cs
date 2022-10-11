using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Clients.Dto
{
    [AutoMapFrom(typeof(Client))]
    public class ClientDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }

}
