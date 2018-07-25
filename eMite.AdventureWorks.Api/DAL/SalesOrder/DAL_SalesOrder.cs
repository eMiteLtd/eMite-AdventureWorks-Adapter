using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eMite.Framework.Bcl.Base.DTO;
using eMite.Framework.Bcl.Base.FC;
using eMite.Framework.Bcl.DTO;
using bcl = eMite.Framework.Bcl;
using Dapper;

namespace eMite.AdventureWorks.Api.DAL.SalesOrder
{
    public class DAL_SalesOrder : BASE.DAL.DAL_SalesOrderBase
    {
        #region "Properties"
        
        #endregion

        #region "Constructors"
        /// <summary>
        /// Constructor accepting adapter configurations
        /// </summary>
        /// <param name="config"></param>
        public DAL_SalesOrder(DTO.DTO_Config config): base(config)
        {
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
            bcl.DTO.DTO_Result Results = new DTO_Result();

            //defines SalesOrderList object to hold all sales orders returned by the poll.
            DTO.SalesOrder.DTO_SalesOrderList SalesOrders = new DTO.SalesOrder.DTO_SalesOrderList();

            //filter criteria object should be of type FC_SalesOrder
            if (filterCriteria.GetType() == typeof(FC.SalesOrder.FC_SalesOrder))
            {
                //cast the filter criteria to the FC_SalesOrder type.
                FC.SalesOrder.FC_SalesOrder cSalesOrder = (FC.SalesOrder.FC_SalesOrder)filterCriteria;

                #region "Sql"

                string query = @"
                                SELECT h.[SalesOrderID]
                                      ,h.[RevisionNumber]
                                      ,h.[OrderDate]
                                      ,h.[DueDate]
                                      ,h.[ShipDate]
                                      ,h.[Status]
                                      ,h.[OnlineOrderFlag]
                                      ,h.[SalesOrderNumber]
                                      ,h.[PurchaseOrderNumber]
                                      ,h.[AccountNumber]
                                      ,h.[CustomerID]
                                      ,h.[SalesPersonID]
                                      ,h.[TerritoryID]
                                      ,h.[BillToAddressID]
                                      ,h.[ShipToAddressID]
                                      ,h.[ShipMethodID]									  
                                      ,h.[CreditCardID]
                                      ,h.[CreditCardApprovalCode]
                                      ,h.[CurrencyRateID]
                                      ,h.[SubTotal]
                                      ,h.[TaxAmt]
                                      ,h.[Freight]
                                      ,h.[TotalDue]
                                      ,h.[Comment]
                                      ,h.[rowguid] HeaderRowGuid
                                      ,h.[ModifiedDate]

									  ,ISNULL(e.FirstName,'') + ' ' + ISNULL(e.MiddleName,'') + ' ' + ISNULL(e.LastName,'') SalesPerson
									  ,ISNULL(p.FirstName,'') + ' ' + ISNULL(p.MiddleName,'') + ' ' + ISNULL(p.LastName,'') CustomerName
									  ,t.CountryRegionCode + '-' + t.Name Territory
                                      ,sh.Name ShipMethod

                                FROM Sales.SalesOrderHeader h --INNER JOIN Sales.SalesOrderDetail d ON h.SalesOrderID = d.SalesOrderID
											LEFT OUTER JOIN Sales.Customer c ON h.CustomerID = c.CustomerID
											LEFT OUTER JOIN Person.Person p ON c.PersonID = p.BusinessEntityID
											LEFT OUTER JOIN Person.Person e ON h.SalesPersonID = e.BusinessEntityID
											LEFT OUTER JOIN Purchasing.ShipMethod sh ON h.ShipMethodID = sh.ShipMethodID
											LEFT OUTER JOIN Sales.SalesTerritory t ON h.TerritoryID = t.TerritoryID
                                WHERE h.ModifiedDate > @StartDate AND h.ModifiedDate <= @EndDate
                                ";


                #endregion

                IEnumerable<dynamic> results = null;
                
                //run the query (via dapper) on the Adventure Works Database and fetch the results.
                using (var conn = new System.Data.SqlClient.SqlConnection(Config.AdventureWorksConnection))
                {                    
                    results = conn.Query<dynamic>(query, new { StartDate = cSalesOrder.StartDate, EndDate = cSalesOrder .EndDate});
                }


                //we'll use the Slapper.AutoMapper to map results into the SalesOrder Object. The automapper can map data into a complex DTO hierarchy
                Slapper.AutoMapper.Cache.ClearInstanceCache();
                
                //specify the primary key columns
                Slapper.AutoMapper.Configuration.AddIdentifiers(typeof(DTO.SalesOrder.DTO_SalesOrder), new List<string> { "SalesOrderID" });

                //map the results into the SalesOrders List object. 
                SalesOrders = new DTO.SalesOrder.DTO_SalesOrderList(Slapper.AutoMapper.MapDynamic<DTO.SalesOrder.DTO_SalesOrder>(results));
                
                //fill in the results object for returning
                Results = new DTO_Result(true, SalesOrders);
                

            }
            else
            {
                //Filter criteria supplied was not of type FC_SalesOrder
                Results = new DTO_Result(false, "Filter criteria not of type: FC_SalesOrder");
                Log.Error("Filter criteria not of type: FC_SalesOrder");
            }

            
            return Results;
        }

        /// <summary>
        /// Sends data for onboarding into eMite
        /// </summary>
        /// <param name="salesOrders"></param>
        /// <returns></returns>
        public override DTO_Result Onboard(DTO.SalesOrder.DTO_SalesOrderList salesOrders)
        {
            bcl.DTO.DTO_Result Results = new DTO_Result();
            
            //We'll use the onboarding helper to onbaord a list of sales order data
            Results = OnboardingHelper.SendData(salesOrders, 
                                                BM.Helpers.BM_SalesOrderDataTableHelper.SalesOrderIndexerConfig.PrimaryKey, 
                                                BM.Helpers.BM_SalesOrderDataTableHelper.SalesOrderIndexerConfig.DateController,
                                                measureFields: BM.Helpers.BM_SalesOrderDataTableHelper.SalesOrderIndexerConfig.MeasureFields);

            return Results;
        }


        /// <summary>
        /// Sends a single SalesOrder object for onboarding into eMite. 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override DTO_Result Onboard(DTO_Base data)
        {
            bcl.DTO.DTO_Result Results = new DTO_Result();

            //Create a sales order list object
            DTO.SalesOrder.DTO_SalesOrderList SalesOrders = new DTO.SalesOrder.DTO_SalesOrderList();

            //check if the DTO passed is of type DTO_SalesOrder
            if (data.GetType() == typeof(DTO.SalesOrder.DTO_SalesOrder))
            {
                //cast the dto to DTO_SalesOrder object
                DTO.SalesOrder.DTO_SalesOrder salesOrder = (DTO.SalesOrder.DTO_SalesOrder)data;

                //add the sales order to the list
                SalesOrders.Add(salesOrder);

                //onboard the sales order.
                Results = Onboard(SalesOrders);

            }
            else
            {
                //Log DTO not of type DTO_SalesOrder
                Log.Error("Record not of type: DTO_SalesOrder");
            }


            return Results;
        }

        #endregion


    }
}
