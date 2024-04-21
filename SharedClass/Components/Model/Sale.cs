﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Model
{
    public class SaleOrder : BaseRecord
    {
        public string SaleOrderID { get; set; }
        public string SaleOrderName { get; set; }
        public string CustomerID { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public string Status { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }

    public class SaleOrderItems : BaseRecord 
    {
        public bool Selected { get; set; }
        public string SaleOrderID { get; set; }
        public string Item { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
    }

    public class SalesInvoice : BaseRecord 
    {
        public string SalesInvoiceID { get; set; }
        public string SalesInvoiceName { get; set; }
        public string CustomerID { get; set; }
        public string Status { get; set; }
        public string RefrenceDocument { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime? DocumentDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsPartiallyPaid { get; set; }
        public bool IsReturn { get; set; }
    }

    public class SaleInvoiceItems : BaseRecord
    {
        public bool Selected { get; set; }
        public string SalesInvoiceID { get; set; }
        public string Item { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
    }
}