using Client.Server.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Server.Common.Responses
{
    public class GetProductsResponse
    {
        public required List<ProductDto> ProductDtos { get; set; }
    }
}
