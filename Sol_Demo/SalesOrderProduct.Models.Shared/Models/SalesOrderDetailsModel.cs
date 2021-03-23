using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SalesOrderProduct.Models.Shared.Models
{
    public class SalesOrderDetailsModel
    {
        public int? SalesOrderID { get; set; }

        public int? ProductID { get; set; }

        public int? OrderQty { get; set; }

        public decimal? UnitPrice { get; set; }

        public ProductModel Product { get; set; }
    }
}