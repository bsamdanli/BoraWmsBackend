using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Storages
{
    [Table("Storage")]
    public class Storage : Entity<int>
    {
        public string Name { get; set; }
        public string StorageCode { get; set; }
        public string StorageType { get; set; }

    }


}
