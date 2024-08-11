using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Server.Common.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public string? CompanyName { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }
    }
}
