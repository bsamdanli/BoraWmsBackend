using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BoraWmsNew.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Receiving.Dto
{
    [AutoMapFrom(typeof(DocumentDetail))]
    public class DocumentDetailDto: EntityDto<int>
    {

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Document Document { get; set; }
        public int DocumentId { get; set; }
        public int Quantity { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
    }

}
