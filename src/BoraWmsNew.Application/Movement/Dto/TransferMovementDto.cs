using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoraWmsNew.StockMovements
{
    public class TransferMovementDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public int StorageInId { get; set; }
        public int StorageOutId { get; set; }


    }
}
