﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bcl = eMite.Framework.Bcl;

namespace eMite.AdventureWorks.Api.DTO.SalesOrder
{
    public class DTO_SalesOrder: bcl.Base.DTO.DTO_Base
    {
        #region "Properties"
        public int SalesOrderID { get; set; }
        public int RevisionNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ShipDate { get; set; }
        public int Status { get; set; }
        public bool OnlineOrderFlag { get; set; }
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerID { get; set; }
        public int SalesPersonID { get; set; }
        public int TerritoryID { get; set; }
        public int BillToAddressID { get; set; }
        public int ShipToAddressID { get; set; }
        public int ShipMethodID { get; set; }
        public int CreditCardID { get; set; }
        public string CreditCardApprovalCode { get; set; }
        public int CurrencyRateID { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmt { get; set; }
        public decimal Freight { get; set; }
        public decimal TotalDue { get; set; }
        public string Comment { get; set; }
        public Guid HeaderRowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string SalesPerson { get; set; }
        public string CustomerName { get; set; }
        public string Territory { get; set; }
        public string ShipMethod { get; set; }
                

        #endregion
    }
}
