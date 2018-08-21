using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMite.AdventureWorks.Api.BM.Misc
{
    public class Utilities
    {

        #region "Properties"
        
        public static BM.Helpers.BM_AdapterData BmAdapterData { get; set; }
        public static BM.Helpers.BM_Authentication BmAuthentication { get; set; }
        #endregion

        #region "Methods"

        /// <summary>
        /// Initialize is called once during the start of the adapter to initialize shared data and business modules 
        /// </summary>
        /// <param name="config"></param>
        public static void Initialize(DTO.DTO_Config config)
        {
            if (BmAdapterData == null)
                BmAdapterData = new Helpers.BM_AdapterData(config);

            if (BmAuthentication == null)
                BmAuthentication = new Helpers.BM_Authentication();
        }

        #endregion

    }
}
