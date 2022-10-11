using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using BoraWmsNew.Products;
using BoraWmsNew.Storages;
using System;
using BoraWmsNew.StockMovements;

namespace BoraWmsNew.StockMovements
{
    [AutoMapFrom(typeof(StockMovement))]
    public class StockMovementDto
    {

        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public MovementType Type { get; set; }

        public int StorageId { get; set; }
        public DateTime CreationTime { get; set; }



    }
}
