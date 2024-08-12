using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Server.Common.Requests
{
    public class GetOrdersRequest
    {
        public DateTime FromDate { get; set; } 
        public DateTime ToDate { get; set; }
        public string CompanyName { get; set; }
    }
}
