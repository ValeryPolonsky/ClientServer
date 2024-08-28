using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Server.Common.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public required int Quantity { get; set; }
    }
}
