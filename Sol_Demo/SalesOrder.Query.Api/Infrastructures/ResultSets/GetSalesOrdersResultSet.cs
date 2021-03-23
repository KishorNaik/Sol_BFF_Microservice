using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Query.Api.Infrastructures.ResultSets
{
    public class GetSalesOrdersResultSet
    {
        public int? SalesOrderID { get; set; }

        public String SalesOrderNumber { get; set; }

        public String PurchaseOrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? ProductID { get; set; }

        public int? OrderQty { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}