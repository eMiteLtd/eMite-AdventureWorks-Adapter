using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eMite.Framework.Bcl.Base.DTO;
using eMite.Framework.Bcl.Base.FC;
using eMite.Framework.Bcl.DTO;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.BM.SalesOrder
{
    public class BM_SalesOrder : BASE.BM.BM_SalesOrderBase
    {
        #region "Properties"

        #endregion

        #region "Constructors"

        /// <summary>
        /// Constructor accepting adapter configurations
        /// </summary>
        /// <param name="config"></param>
        public BM_SalesOrder(DTO.DTO_Config config): base(config)
        {
            //initialize the Sales Order DAL
            Dal = new DAL.SalesOrder.DAL_SalesOrder(config);
        }
        #endregion

        #region "Overrides"

        /// <summary>
        /// Gets sales orders using the filter criteria supplied. 
        /// </summary>
        /// <param name="filterCriteria"></param>
        /// <returns></returns>
        public override DTO_Result Get(FC_Base filterCriteria)
        {
            return Dal.Get(filterCriteria);
        }

        /// <summary>
        /// Sends data for onboarding into eMite
        /// </summary>
        /// <param name="salesOrders"></param>
        /// <returns></returns>
        public override DTO_Result Onboard(DTO.SalesOrder.DTO_SalesOrderList salesOrders)
        {
            return Dal.Onboard(salesOrders);
        }

        /// <summary>
        /// Sends a single SalesOrder object for onboarding into eMite. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DTO_Result Onboard(DTO_Base data)
        {
            return Dal.Onboard(data);
        }

        #endregion


    }
}
