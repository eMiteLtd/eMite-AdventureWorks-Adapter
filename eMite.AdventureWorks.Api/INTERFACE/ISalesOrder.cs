using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.INTERFACE
{
    interface ISalesOrder
    {
        bcl.DTO.DTO_Result Get(bcl.Base.FC.FC_Base filterCriteria);
        bcl.DTO.DTO_Result Onboard(bcl.Base.DTO.DTO_Base data);
        bcl.DTO.DTO_Result Onboard(DTO.SalesOrder.DTO_SalesOrderList salesOrders);
    }
}
