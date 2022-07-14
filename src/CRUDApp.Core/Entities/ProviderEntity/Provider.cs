using CRUDApp.Core.Entities.OrderEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApp.Core.Entities.ProviderEntity
{
    public class Provider : BaseEntity
    {
        public ICollection<Order> Orders { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Name { get; set; }

        public Provider(string name)
        {
            Orders = new List<Order>();
            Name = name;
        }
    }
}
