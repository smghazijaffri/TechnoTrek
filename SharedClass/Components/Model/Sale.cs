namespace SharedClass.Components.Model
{
    public class SaleOrder : BaseRecord
    {
        public string? SaleOrderID { get; set; }
        public string? SaleOrderName { get; set; }
        public string? CustomerID { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public string? Status { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }

    public class SO_Item : BaseRecord
    {
        public bool Selected { get; set; }
        public string? SaleOrderID { get; set; }
        public string? Item { get; set; }
        public int RowID { get; set; }
        public string? UOM { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
    }

    public class SalesInvoice : BaseRecord
    {
        public string? SalesInvoiceID { get; set; }
        public string? SalesInvoiceName { get; set; }
        public string? CustomerID { get; set; }
        public string? Status { get; set; }
        public string? RefrenceDocument { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsPartiallyPaid { get; set; }
        public bool IsReturn { get; set; }
    }

    public class SI_Item : BaseRecord
    {
        public bool Selected { get; set; }
        public string? SalesInvoiceID { get; set; }
        public string? Item { get; set; }
        public string? UOM { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
    }
    public class Customer : BaseRecord
    {
        public string? CustomerID { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
       public string? Contact { get; set; }
       public string? CustomerType { get; set; }
        
    }

    public class GoodsIssue : BaseRecord
    {
        public string? GoodsIssueID { get; set; }
        public string? GoodsIssueName { get; set; }
        public string? CustomerID { get; set; }
        public string? Status { get; set; }
        public bool IsReturn { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public string? RefrenceDocument { get; set; }
        public DateTime DocumentDate { get; set; }
    }

    public class GI_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string? GoodsIssueID { get; set; }
        public string? Item { get; set; }
        public string? UOM { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
    }
}
