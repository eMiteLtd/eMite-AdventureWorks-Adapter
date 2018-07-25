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
           
            //string IndexName = "IncidentTickets";

            ////Create a datatable to hold data for onboarding.
            //DataTable Incidents = GetSampleData(IndexName);

            ////identify long tet fields in the datatable 
            //List<string> LongTextFields = new List<string>() { "Description" };
            ////identify additional measure fields that are desired. 
            //List<string> MeasureFields = new List<string>() { "ReassignmentCount" };

            ////populate the TransactionData DTO
            //eob.DTO.DTO_OnboardingTransactionData OnboardingOpenTicketsData = new Framework.Bcl.Onboarding.DTO.DTO_OnboardingTransactionData(Incidents,
            //                                                                                                                                "TicketNumber",
            //                                                                                                                                "LastUpdate",
            //                                                                                                                                config.ClassDefinition,
            //                                                                                                                                IndexName,
            //                                                                                                                                LongTextFields,
            //                                                                                                                                MeasureFields,
            //                                                                                                                                true);

            ////load the TransactionData DTO into the TransactionList DTO object that will be sent for onboarding.
            //eob.DTO.DTO_TransactionList OpenTickets = new Framework.Bcl.Onboarding.DTO.DTO_TransactionList(OnboardingOpenTicketsData);

            //if (OpenTickets.Items.Count > 0)
            //{
            //    DataTransferObject = new DTO_Transfer(OpenTickets);
            //}

            #endregion

            return DataTransferObject;

        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Creates a sample data table that will hold data fetched from the data source.
        /// </summary>
        /// <returns></returns>
        private DataTable GetSampleData(string tableName)
        {

            DataTable dt = new DataTable(tableName);

            //Important: define the key column in the datatable. 
            DataColumn KeyColumn = new DataColumn("TicketNumber", typeof(string));

            dt.Columns.Add(KeyColumn);
            dt.Columns.Add("AssignedTo", typeof(string));
            dt.Columns.Add("AssignmentGroup", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("ReassignmentCount", typeof(int));
            dt.Columns.Add("LastUpdate", typeof(DateTime));

            DataColumn[] Keys = new DataColumn[] { KeyColumn };

            //set the key columns
            dt.PrimaryKey = Keys;

            //populate 2 sample incident ticket data for onboarding.
            dt.Rows.Add("INC-0001", "Alex Taylor", "Release Management", "Sample Description 1 - Transaction Adapter", 2, DateTime.UtcNow);
            dt.Rows.Add("INC-0002", "Michael", "IT - Support", "Sample Description 2 - Transaction Adapter", 1, DateTime.UtcNow);


            return dt;

        }


        #endregion

    }
}
