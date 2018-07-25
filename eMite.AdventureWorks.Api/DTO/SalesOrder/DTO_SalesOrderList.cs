using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eMite.AdventureWorks.Api.DTO.SalesOrder
{
    public class DTO_SalesOrderList:List<DTO_SalesOrder>
    {

        public DTO_SalesOrderList()
        {

        }

        public DTO_SalesOrderList(IEnumerable<DTO.SalesOrder.DTO_SalesOrder> orders)
        {
            if (orders != null)
            {
                this.AddRange(orders);
            }
        }


    }
}
