using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eMite.Framework.Bcl.Base.DTO;
using eMite.Framework.Bcl.Base.FC;
using eMite.Framework.Bcl.DTO;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.BASE.DAL
{
    public abstract class DAL_SalesOrderBase : bcl.Base.BM.BM_NoCrudBase, INTERFACE.ISalesOrder,IDisposable
    {
        #region "Properties"
        protected DTO.DTO_Config Config { get; set; }
        protected Api.BM.Helpers.BM_OnboardingHelper OnboardingHelper { get; set; }
        #endregion

        #region "Constructors"

        /// <summary>
        /// Constructor taking adapter configurations
        /// </summary>
        /// <param name="config"></param>
        public DAL_SalesOrderBase(DTO.DTO_Config config)
        {
            Config = config;
            
            //Initialize the data onboarding helper.
            OnboardingHelper = new Api.BM.Helpers.BM_OnboardingHelper(Config);
        }


        #endregion

        #region "Abstract Methods"
        public abstract DTO_Result Get(FC_Base filterCriteria);
        public abstract DTO_Result Onboard(DTO_Base data);
        public abstract DTO_Result Onboard(DTO.SalesOrder.DTO_SalesOrderList salesOrders);
        public virtual void Dispose()
        {
            //dispose the onboarding helper.
            OnboardingHelper = null;
        }


        #endregion
    }
}
