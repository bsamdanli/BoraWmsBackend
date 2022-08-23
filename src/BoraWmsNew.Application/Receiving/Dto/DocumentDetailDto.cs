using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoraWmsNew.Receiving.Dto
{
    [AutoMapFrom(typeof(DocumentDetail))]
    public class DocumentDetailDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int DocumentId { get; set; }
        public int Quantity { get; set; }
    }

}
