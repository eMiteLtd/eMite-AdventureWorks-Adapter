using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eMite.Framework.Bcl.Base.DTO;
using eMite.Framework.Bcl.Base.FC;
using eMite.Framework.Bcl.DTO;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.BASE.BM
{
    public abstract class BM_SalesOrderBase: bcl.Base.BM.BM_NoCrudBase, INTERFACE.ISalesOrder
    {
        #region "Properties"
        private DTO.DTO_Config Config { get; set; }
        public BASE.DAL.DAL_SalesOrderBase Dal { get; set; }
        #endregion

        #region "Constructors"
        public BM_SalesOrderBase(DTO.DTO_Config config)
        {
            Config = config;
        }


        #endregion

        #region "Abstract Methods"
        public abstract DTO_Result Get(FC_Base filterCriteria);
        public abstract DTO_Result Onboard(DTO_Base data);
        public abstract DTO_Result Onboard(DTO.SalesOrder.DTO_SalesOrderList salesOrders);
        #endregion
    }
}
