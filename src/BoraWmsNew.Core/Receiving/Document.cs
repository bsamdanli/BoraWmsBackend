using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoraWmsNew.Clients;
using BoraWmsNew.Products;

namespace BoraWmsNew.Receiving
{
    public class Document : FullAuditedEntity<int>
    {


        public Client Client { get; set; }
        public int ClientId { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
