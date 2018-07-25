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
using eMite.Framework.Bcl.Configurations.DTO;

namespace eMite.AdventureWorks.Adapter.BM
{
    //############################################################################################################
    //This is a template generated partial class that contains adapter related code which should not be changed 
    //at any time. Please execise caution and contact eMite if you do need to change this code.
    //############################################################################################################

    public partial class BM_Plugin : addinview.BASE.BM.BM_AdapterBase
    {
        #region "Properties"

        public DTO.DTO_Config config { get; set; }
        public DateTime SavePoint { get; set; }

        #endregion

        #region "Constructors"
        /// <summary>
        /// Constructor to initialize configurations
        /// </summary>
        public BM_Plugin()
        {
            DtoConfig = new DTO.DTO_Config();

        }

        /// <summary>
        /// Static constructor that runs before the normal constructor to initialize the framework libraries.
        /// </summary>
        static BM_Plugin()
        {

        }


        #endregion

        #region "Overrides"


        #region "Do not change"   

        /// <summary>
        /// This method is used by when the configuration loader is ran to save adapter templates to the configurations table.
        /// </summary>
        public override string GetAdapterTemplate()
        {
            return bcl.Common.BM.BM_SerializationHelper.Serialize(GetTemplate(this.GetType()));
        }

        /// <summary>
        /// This method is called by the adapter framework to pass in initialization data to the adapter when it is activated. 
        /// </summary>
        /// <param name="dtoHostToAddIn">serialized data for adapter initialization</param>
        public override void InitializeAdapterData(string dtoHostToAddIn)
        {
            #region "Initialization"

            HostToAddInData = bcl.Common.BM.BM_SerializationHelper.Deserialize<addinview.DTO.DTO_HostToAddIn>(dtoHostToAddIn);

            DtoConfig.LoadConfig(HostToAddInData.Config);
            DtoConfig.LoadSavePoint(HostToAddInData.SavePoint);

            Log = new bcl.BM.BM_Logging();

            #endregion

            #region "Config and Save Point"

            //Cast the configurations into the correct config object
            config = (DTO.DTO_Config)DtoConfig;

            //get the default save point
            SavePoint = config.StartDateUTC;
            DateTime tmpSavePoint = SavePoint;

            //check and use the save point from the adapter framework based on the last run.
            if (config.SavePoint != null)
            {
                if (!string.IsNullOrEmpty(config.SavePoint.SavePointObject[0].Value))
                    if (DateTime.TryParse(config.SavePoint.SavePointObject[0].Value, out tmpSavePoint))
                        Log.Error("Save point could not be parsed: " + config.SavePoint.SavePointObject[0].Value + ". Using default");
            }

            SavePoint = tmpSavePoint;


            #endregion

        }
        #endregion

        #endregion



    }
}
