using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using BoraWmsNew.Products;
using BoraWmsNew.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.StockMovements
{
    public class StockMovement : FullAuditedEntity<int>
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public MovementType Type { get; set; }

        public Storage Storage { get; set; }
    }
    public enum MovementType
    {
        In = 1,
        Out = 2
    }
}
