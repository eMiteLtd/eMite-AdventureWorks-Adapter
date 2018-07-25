using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.FC.SalesOrder
{
    public class FC_SalesOrder : bcl.Base.FC.FC_Base
    {   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public FC_SalesOrder() : base(null)
        {

        }
    }
}
