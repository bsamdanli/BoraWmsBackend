using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Products
{
    [Table("Products")]
    public class Product : Entity<int>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string StockCode { get; set; }
    }

}
