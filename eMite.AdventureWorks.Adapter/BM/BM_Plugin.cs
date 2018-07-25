using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.AddIn;
using addinview = eMite.Framework.Bcl.Adapters.AddInView;
using cfg = eMite.Framework.Bcl.Configurations;
using bcl = eMite.Framework.Bcl;
using adp = eMite.Framework.Bcl.Adapters.Common;
using eob = eMite.Framework.Bcl.Onboarding;
using System.Data;
using System.Data.SqlClient;
using eMite.Configuration;
using eMite.Framework.Bcl.Onboarding.DTO;
using Api = eMite.AdventureWorks.Api;

namespace eMite.AdventureWorks.Adapter.BM
{
    [AddIn("AdventureWorks")]
    public partial class BM_Plugin : addinview.BASE.BM.BM_AdapterBase
    {
        #region "Overrides"

        /// <summary>
        /// The Main() method is the key entry point for the business logic that the adapter provides to the adapter framework. 
        /// This method is called by the adapter framework to process and provide data for onboarding into the eMite database or indexes.
        /// </summary>
        /// <returns>DTO_Transfer object for onboarding into eMite</returns>

        public override DTO_Transfer Main()
        {
            Log.Info("-----------");

            eob.DTO.DTO_Transfer DataTransferObject = null;

            #region "***** Get Data *****" 

            Api.BM.BM_AdapterController BmAdapterController = new Api.BM.BM_AdapterController(config);
            BmAdapterController.Start();          

            #endregion

            return DataTransferObject;

        }

        #endregion

      

    }
}
