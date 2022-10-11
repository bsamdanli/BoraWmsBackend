using Abp.Domain.Entities.Auditing;
using BoraWmsNew.Products;
using BoraWmsNew.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoraWmsNew.StockMovements;

namespace BoraWmsNew.StockMovements
{

    public class StockReportDto : FullAuditedEntity<int>
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string StorageCode { get; set; } //dtoları oluşacak.
        public MovementType Type { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
