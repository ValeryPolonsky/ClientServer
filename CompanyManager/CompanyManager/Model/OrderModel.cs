using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManager.Model
{
    public class OrderModel
    {
        public int Id { get; set; }

        public string? CompanyName { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }
    }
}
