using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;
using eob = eMite.Framework.Bcl.Onboarding;

namespace eMite.AdventureWorks.Api.BM.Helpers
{
    public class BM_OnboardingHelper:bcl.Adapters.Common.BASE.DAL.DAL_NoCrudAdapterBase
    {
        #region "Properties"
        public Api.DTO.DTO_Config Config { get; set; }
        public Api.BM.Helpers.BM_SalesOrderDataTableHelper SalesOrderDataTableHelper { get; set; }

        #endregion


        #region "Constructors"
        /// <summary>
        /// Constructor accepting adpater configurations
        /// </summary>
        /// <param name="config"></param>
        public BM_OnboardingHelper(Api.DTO.DTO_Config config)
            //:base(new bcl.Messaging.DTO.DTO_MessageConfig("AdventureWorksAdapter","Dashboard","LiveData",config.IndexGroup))
        {
            Config = config;

            //initialize the sales order datatable helper.
            SalesOrderDataTableHelper = new BM.Helpers.BM_SalesOrderDataTableHelper();


        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Send the SalesOrderList to the data onboarding web service. This method accepts a SalesOrderList rather than a DataTable
        /// </summary>
        /// <param name="salesOrders">Sales Order Data List</param>
        /// <param name="primaryKey">Primary key field in the data</param>
        /// <param name="dateController">DateController field in the data</param>
        /// <param name="measureFields">List of measure fields in the data</param>
        /// <param name="longTextFields">List of long text fields in the data</param>
        /// <param name="commaAnalyzerFields">List of comma analyzer (CSV) fields in the data</param>
        /// <returns></returns>
        public bcl.DTO.DTO_Result SendData(DTO.SalesOrder.DTO_SalesOrderList salesOrders, string primaryKey, string dateController, List<string> measureFields = null, List<string> longTextFields = null, List<string> commaAnalyzerFields = null)
        {
            bcl.DTO.DTO_Result Results = new Framework.Bcl.DTO.DTO_Result(false);

            //Add the data to the Sales order datatable helper. 
            SalesOrderDataTableHelper.AddSalesOrders(salesOrders);

            //check if the datatable is not empty
            if (SalesOrderDataTableHelper.SalesOrderTable != null)
            {
                if (SalesOrderDataTableHelper.SalesOrderTable.Rows.Count > 0)
                {
                    //send the datatable for onboarding via the eMite data onboarding web service. 
                    Results = SendData(SalesOrderDataTableHelper.SalesOrderTable, primaryKey, dateController, measureFields, longTextFields, commaAnalyzerFields);
                    SalesOrderDataTableHelper.Dispose();

                }
                else
                    Results = new bcl.DTO.DTO_Result(false, "No records to index");
            }
            else
                Results = new bcl.DTO.DTO_Result(false, "No records to index");

            return Results;

        }


        /// <summary>
        /// Send a DataTable to the data onboarding web service (Generic data onboarding)
        /// </summary>
        /// <param name="data">DataTable containg the data to onboard</param>
        /// <param name="primaryKey">Primary key field in the data</param>
        /// <param name="dateController">DateController field in the data</param>
        /// <param name="measureFields">List of measure fields in the data</param>
        /// <param name="longTextFields">List of long text fields in the data</param>
        /// <param name="commaAnalyzerFields">List of comma analyzer (CSV) fields in the data</param>
        /// <returns></returns>
        public bcl.DTO.DTO_Result SendData(DataTable data, string primaryKey, string dateController, List<string> measureFields = null, List<string> longTextFields = null, List<string> commaAnalyzerFields = null)
        {
            bcl.DTO.DTO_Result Results = new Framework.Bcl.DTO.DTO_Result(false);

            //check if data table is not empty
            if (data != null)
            {
                //create a DataTransferObject that can be used to transfer standard data structure to the onboarding web service. 
                eob.DTO.DTO_Transfer DataTransferObject = null;

                //Log and throw an exception if the DataTable is not given a name. The name will be used to create the elastic index
                string TableName = "";
                if (string.IsNullOrEmpty(data.TableName))
                {
                    Log.Error("Data TableName cannot be empty!");
                    throw new Exception("Data TableName cannot be empty!");
                }
                else
                    TableName = data.TableName;

                //populate the TransactionData helper DTO
                eob.DTO.DTO_OnboardingTransactionData TransactionData = new Framework.Bcl.Onboarding.DTO.DTO_OnboardingTransactionData(data,
                                                                                                                                       primaryKey,
                                                                                                                                       dateController,
                                                                                                                                       Config.ClassDefinition,
                                                                                                                                       TableName,
                                                                                                                                       longTextFields,
                                                                                                                                       measureFields,
                                                                                                                                       true,
                                                                                                                                       commaAnalyzerFields: commaAnalyzerFields);

                //load the TransactionData helper DTO into the TransactionList DTO object that will be sent for onboarding.
                eob.DTO.DTO_TransactionList TransactionList = new Framework.Bcl.Onboarding.DTO.DTO_TransactionList(TransactionData);

                if (TransactionList.Items.Count > 0)
                {
                    //load the data transfer object
                    DataTransferObject = new eob.DTO.DTO_Transfer(TransactionList);

                    //Send data for onboarding.
                    Results = OnboardData(DataTransferObject);                 
                    
                }
            }

            //return results.
            return Results;

        }
        

        #endregion

    }
}
