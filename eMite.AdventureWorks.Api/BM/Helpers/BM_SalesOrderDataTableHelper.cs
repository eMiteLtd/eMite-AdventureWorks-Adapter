using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.BM.Helpers
{
    public class BM_SalesOrderDataTableHelper: IDisposable
    {
        #region "Properties"        
        public static DTO.DTO_IndexerConfig SalesOrderIndexerConfig { get; set; }
        public DataTable SalesOrderTable { get; set; }

        public object SalesOrderTableLock = new object();
        #endregion

        #region "Constructors"
        public BM_SalesOrderDataTableHelper()
        {           
            
        }

        static BM_SalesOrderDataTableHelper()
        {
            //define measure fields in the Sales Order Data.
            List<string> MeasureFields = new List<string>() { "SubTotal", "TaxAmount", "Freight", "TotalDue" };            

            //Define the IndexName, PrimaryKey and DateController fields in the SalesOrder Data.
            SalesOrderIndexerConfig = new DTO.DTO_IndexerConfig("SalesOrder", "SalesOrderID", "ModifiedDate", MeasureFields);
        }
        #endregion

        #region "Methods"

        /// <summary>
        /// Helper method to create a DataTable for Sales Order Data.
        /// </summary>
        /// <param name="tableName">Table Name</param>
        private void GenerateSalesOrderDatatable(string tableName)
        {
            SalesOrderTable = new DataTable(tableName);

            Type t = typeof(DTO.SalesOrder.DTO_SalesOrder);

            // Get the public properties.
            PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //Add columns
            foreach (var propInfo in propInfos)
            {
                SalesOrderTable.Columns.Add(propInfo.Name, propInfo.PropertyType);
            }
            

        }

        /// <summary>
        /// Adds a list of Sales Orders to the SalesOrder DataTable
        /// </summary>
        /// <param name="salesOrders"></param>
        public void AddSalesOrders(DTO.SalesOrder.DTO_SalesOrderList salesOrders)
        {
            lock (SalesOrderTableLock)
            {
                //Generate table if it does not exist.
                if (SalesOrderTable == null)
                    GenerateSalesOrderDatatable(SalesOrderIndexerConfig.IndexName);

                //check and add all sales orders to the datatable.
                if (salesOrders != null)
                {
                    foreach (var salesOrder in salesOrders)
                    {
                        AddSalesOrder(salesOrder);
                    }
                }
            }

        }

        /// <summary>
        /// Add a single sales order to the SalesOrder Datatable
        /// </summary>
        /// <param name="salesOrder"></param>
        public void AddSalesOrder(DTO.SalesOrder.DTO_SalesOrder salesOrder)
        {
            lock (SalesOrderTableLock)
            {
                //Generate datatable if it does not exists
                if (SalesOrderTable == null)
                    GenerateSalesOrderDatatable(SalesOrderIndexerConfig.IndexName);

                DataRow NewRow = SalesOrderTable.NewRow();

                Type t = typeof(DTO.SalesOrder.DTO_SalesOrder);
                // Get the public properties.
                PropertyInfo[] propInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                //Load new row with data.
                foreach (var propInfo in propInfos)
                {
                    NewRow[propInfo.Name] = propInfo.GetValue(salesOrder);
                }

                //add new row to datatable.
                SalesOrderTable.Rows.Add(NewRow);
            }

        }

        /// <summary>
        /// Dispose managed objects.
        /// </summary>
        public void Dispose()
        {
            SalesOrderTable = null;
        }
        
        #endregion
    }
}
