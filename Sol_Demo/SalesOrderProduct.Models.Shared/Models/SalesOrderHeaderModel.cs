using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SalesOrderProduct.Models.Shared.Models
{
    public class SalesOrderHeaderModel
    {
        public int? SalesOrderID { get; set; }

        public String SalesOrderNumber { get; set; }

        public String PurchaseOrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public List<SalesOrderDetailsModel> SalesOrderDetails { get; set; }

        #region Non Domain Property

        public ServerPagination Pagination { get; set; }

        #endregion Non Domain Property
    }
}