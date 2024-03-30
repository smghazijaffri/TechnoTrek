namespace SharedClass.Components.Model
{
    public class PurchaseRequisition : BaseRecord
    {
        public string PRNumber { get; set; }
        public string PRname { get; set; }
        public string Status { get; set; }
        public DateTime? DocumentDate { get; set; }
    }

    public class PR_Items : BaseRecord
    {
        public int RowID { get; set; }
        public string UOM { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public bool Selected { get; set; }
        public string PRNumber { get; set; }
        public DateTime? RequiredBy { get; set; }
    }
    public class PurchaseOrders : BaseRecord
    {
        public string PurchaseOrderID { get; set; }
        public string PurchaseOrderName { get; set; }
        public string VendorID { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public string Status { get; set; }
        public string RefrenceDocument { get; set; }
        public DateTime? DocumentDate { get; set; }
    }

    public class PO_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string PurchaseOrderID { get; set; }
        public string Item { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public DateTime? RequiredBy { get; set; }
    }
    public class Vendor : BaseRecord 
    { 
        public string VendorID { get; set; }
        public string VendorName { get; set; }
        public string VendorGroup { get; set; }
        public string VendorType { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
    }
    public class RequestForQuotation : BaseRecord
    {
        public string RFQNumber { get; set; }
        public string RFQName { get; set; }
        public string Status { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string RefrenceDocument { get; set; }


    }
    public class RFQVendor : BaseRecord
    {
        public bool Selected { get; set; }
        public string RFQNumber { get; set; }
        public string VendorID { get; set; }
        public int RowID { get; set; }
        public bool SendEmail { get; set; }
    }
    public class RFQ_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string RFQNumber { get; set; }
        public string Item { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public DateTime? RequiredBy { get; set; }
    }

    public class GoodReceipt : BaseRecord
    {
        public string GoodReceiptID { get; set; }
        public string GoodReceiptName { get; set; }
        public string Status { get; set; }
        public string VendorID { get; set; }
        public string RefrenceDocument { get; set; }
        public int TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public DateTime? DocumentDate { get; set; }
    }

    public class GR_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string GoodReceiptID { get; set; }
        public string Item { get; set; }
        public int RowID { get; set; }
        public int AcceptedQuantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
    }
}