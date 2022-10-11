using Abp.AutoMapper;
using BoraWmsNew.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BoraWmsNew.Receiving.Dto
{
    [AutoMapFrom(typeof(Document))]
    public class ReceivingDto
    {
        public int Id { get; set; }
        public string DocumentNo { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public string Situation { get; set; }
        public string DocumentDate { get; set; }
        public DateTime CreationTime { get; set; }
        public int[] ProductsIds { get; set; }
        public string[] ProductsCodes { get; set; }
        public int[] Quantities { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDeleted { get; set; }
    }

}
