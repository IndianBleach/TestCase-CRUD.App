using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApp.Core.DTOs.OrderDTOs
{
    public class CreateOrderDto
    {
        public string? Number { get; set; }
        public DateTime? Date { get; set; }
        public int? ProviderId { get; set; }

        public CreateOrderDto()
        {
            ProviderId = null;
            Date = null;
        }
    }
}
