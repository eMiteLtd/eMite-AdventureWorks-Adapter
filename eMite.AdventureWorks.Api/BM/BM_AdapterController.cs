using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.BM
{
    public class BM_AdapterController : bcl.Base.BM.BM_NoCrudBase
    {
        #region "Properties"        
        private DTO.DTO_Config Config { get; set; }
        public BM.SalesOrder.BM_SalesOrder BmSalesOrder { get; set; }

        #endregion


        #region "Constructors"
        /// <summary>
        /// Constructor accepting the adapter configurations object. 
        /// </summary>
        /// <param name="config"></param>
        public BM_AdapterController(DTO.DTO_Config config)
        {
            Config = config;
            BmSalesOrder = new BM.SalesOrder.BM_SalesOrder(config);
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// Entry point of the Adventure works adapter. 
        /// </summary>
        public void Start()
        {
            bool ExitLoop = true;
            //get sales order filter criteria
            var SalesOrderFilter = GetQueryRange();

            //loop until EndDate < DateTime.UTCNow
            do
            {
                bcl.DTO.DTO_Result OnboardResults = null;

                Log.Info("StartDate: " + SalesOrderFilter.StartDate.ToString("yyyy-MM-ddHH:mm:ss") + " | EndDate: " + SalesOrderFilter.EndDate.ToString("yyyy-MM-ddHH:mm:ss"));


                //load sales orders using the sale order filter
                var Data = BmSalesOrder.Get(SalesOrderFilter);

                //check if call was successful
                if (Data.Successful)
                {
                    //check if there were results returned for the poll.
                    if (Data.Results != null)
                    {
                        //cast the data into the SalesOrderList Object
                        DTO.SalesOrder.DTO_SalesOrderList SalesOrders = (DTO.SalesOrder.DTO_SalesOrderList)Data.Results;

                        //Send data for onboarding.
                        OnboardResults = BmSalesOrder.Onboard(SalesOrders);

                        //check if data onboarding was successful.
                        if (OnboardResults.Errors != null)
                        {
                            //output error logs if any.
                            if (OnboardResults.Errors.Count > 0)
                            {
                                Log.Warn(OnboardResults.Errors[0]);
                            }
                        }
                    }
                }

                //check if the current poll EndDate is less than UtcNow. So that we can prepare a new filter criteria for the next data poll
                if (SalesOrderFilter.EndDate < DateTime.UtcNow)
                {
                    //set filter criteia for next poll. 
                    SalesOrderFilter.StartDate = SalesOrderFilter.EndDate;
                    //add the configured QueryInterval set by the user for each poll.
                    SalesOrderFilter.EndDate = SalesOrderFilter.StartDate.AddMinutes(Config.QueryInterval);

                    ExitLoop = false;
                }
                else
                    ExitLoop = true;


            } while (ExitLoop == false);

        }

        /// <summary>
        /// Helper method to set the Sales order filter criteria.
        /// </summary>
        /// <returns></returns>
        public FC.SalesOrder.FC_SalesOrder GetQueryRange()
        {            
            //initiate the onboarging helper.
            BM.Helpers.BM_OnboardingHelper OnboardingHelper = new BM.Helpers.BM_OnboardingHelper(Config);

            //get the save point of the SalesOrder Index
            var SavePoint = OnboardingHelper.GetFactQuerySavePoint(Config.IndexGroup, BM.Helpers.BM_SalesOrderDataTableHelper.SalesOrderIndexerConfig.IndexName);

            //set the initial start and end data of the SalesOrder query
            DateTime StartDate = Config.StartDateUTC;
            DateTime EndDate = StartDate.AddMinutes(Config.QueryInterval);

            if (SavePoint.Count > 0)
            {
                //if save point was returned we set the start and end date based on the save point value. 
                StartDate = (DateTime)SavePoint[0].Value;
                EndDate = StartDate.AddMinutes(Config.QueryInterval);
            }

            //Create the Sales Order Filter
            FC.SalesOrder.FC_SalesOrder SalesOrderFilter = new FC.SalesOrder.FC_SalesOrder() { StartDate = StartDate, EndDate = EndDate };

            //return the sales order filter object.
            return SalesOrderFilter;
        }

        #endregion


    }
}
