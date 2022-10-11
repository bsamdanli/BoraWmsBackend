using Abp.AutoMapper;
using Abp.Domain.Entities;
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
    [AutoMapFrom(typeof(StockMovement))]
    public class StockMovementReportDto : Entity<int>
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public MovementType Type { get; set; }

        public Storage Storage { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
