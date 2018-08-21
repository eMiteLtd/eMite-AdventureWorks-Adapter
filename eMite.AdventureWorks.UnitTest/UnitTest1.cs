using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using tran = eMite.AdventureWorks.Adapter;
using addinview = eMite.Framework.Bcl.Adapters.AddInView;

namespace eMite.AdventureWorks.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExtractorTest()
        {

            //initialize the extractor class
            tran.BM.BM_Plugin bmPlugin = new Adapter.BM.BM_Plugin();

            //gets a sample configuration that the developer can change for testing.
            addinview.DTO.DTO_HostToAddIn HostToAddInData = bmPlugin.GetSampleConfig();

            //set instance name. Can be any text for testing
            HostToAddInData.Config.CFGInstances[0].UniqueName = "SampleAdventureWorks";

            //set the storage type to text file for testing purposes
            HostToAddInData.Config.SetConfig("StorageType","ElasticSearch");
            HostToAddInData.Config.SetConfig("StartDateUTC",DateTime.Parse("2005-07-07 00:00:00.000"));
            HostToAddInData.Config.SetConfig("QueryInterval", 259200);
            HostToAddInData.Config.SetConfig("AdventureWorksConnection", "Data Source=SQL5034.site4now.net;Initial Catalog=DB_9F9DA7_adventureworks;User Id=DB_9F9DA7_adventureworks_admin;Password=EmiteTraining123!;");
            

            //initialize the adapter before running.
            bmPlugin.InitializeAdapterData(addinview.BM.BM_Helpers.SerializeHostToAddInData(HostToAddInData));

            //run the adapter logic and get serialized results.
            string results = bmPlugin.Process();
            

        }

        #region "Helpers"

        [TestInitialize()]
        public void Initialize()
        {
            System.Configuration.ConfigurationManager.GetSection("dummy");

        }

        #endregion
    }
}
