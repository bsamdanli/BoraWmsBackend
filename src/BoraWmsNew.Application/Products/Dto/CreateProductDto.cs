using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Products.Dto
{
    [AutoMapTo(typeof(Product))]
    public class CreateProductDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string StockCode { get; set; }
    }

}
