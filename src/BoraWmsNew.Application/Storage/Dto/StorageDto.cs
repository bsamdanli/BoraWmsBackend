using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Storage.Dto
{
    [AutoMapFrom(typeof(Storage))]
    public class StorageDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string StorageCode { get; set; }
        public string StorageType { get; set; }

    }
}
