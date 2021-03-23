using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderProduct.Models.Shared.Models
{
    public class ServerPagination
    {
        #region Non Domain Property

        public int PageNumber { get; set; }

        public int RowsOfPage { get; set; }

        #endregion Non Domain Property
    }
}