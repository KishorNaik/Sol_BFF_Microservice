using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrderProduct.Models.Shared.Models
{
    public class ProductModel
    {
        public int? ProductId { get; set; }

        public String Name { get; set; }

        public String ProductNumber { get; set; }

        #region Non Domain Property

        public String ProductIds { get; set; }
        #endregion Non Domain Property

    }
}