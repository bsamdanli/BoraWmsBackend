using Abp.Domain.Entities.Auditing;
using BoraWmsNew.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoraWmsNew.Receiving
{
    public class DocumentDetail : FullAuditedEntity<int>
    {

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Document Document { get; set; }
        public int DocumentId { get; set; }
        public int Quantity { get; set; }

    }

}
