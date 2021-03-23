using SalesOrderProduct.Models.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderProduct.Models.Shared.Response
{
    public class SalesOrderResponse
    {
        public List<SalesOrderHeaderModel> SalesOrderHeaderModels { get; set; }
    }
}