using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.BM.Helpers
{
    /// <summary>
    /// A wrapper class to allow the adapter to save adapter data (non configurational data) to the eMite database. 
    /// This helper class uses the eMite Configurations API to provide the functionality
    /// </summary>
    public class BM_AdapterData : bcl.Base.BM.BM_NoCrudBase
    {
        #region "Properties"
        public DTO.DTO_Config Config { get; set; }
        public bcl.Configurations.BM.BM_Configurations BmConfig { get; set; }
        private string UniqueName { get; set; }

        #endregion

        #region "Constructors"

        public BM_AdapterData(DTO.DTO_Config config)
        {
            Config = config;

            UniqueName = Config.ClassDefinition.CFGInstances[0].UniqueName + " - AdapterData";

            BmConfig = new bcl.Configurations.BM.BM_Configurations("AdapterData", UniqueName);

        }
        #endregion

        #region "Methods"
        /// <summary>
        /// Adds a key/value pair to the configuration data.
        /// </summary>
        /// <param name="key">Data Key</param>
        /// <param name="value">Data Value</param>
        /// <param name="valueType">Data Type</param>
        public void AddData(string key, object value, bcl.Configurations.BM.BM_Configurations.ValueType valueType)
        {
            string Value = null;

            if (value != null)
                Value = value.ToString();

            BmConfig.AddUpdateKeyValuePair(key, Value, valueType);
        }

        /// <summary>
        /// Gets the configuration data for a given key
        /// </summary>
        /// <param name="key">Data Key</param>
        /// <returns>Data KeyValue object</returns>
        public bcl.Configurations.DTO.DTO_CFGValuePair GetData(string key)
        {
            return BmConfig.GetKeyValuePair(key);
        }

        /// <summary>
        /// Saves the Adapter data into eMite database
        /// </summary>
        public void Save()
        {
            BmConfig.Save();
        }
        #endregion

    }
}
