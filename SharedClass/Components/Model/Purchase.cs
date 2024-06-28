namespace SharedClass.Components.Model
{
    public class PurchaseRequisition : BaseRecord
    {
        public string? PRNumber { get; set; }
        public string? PRname { get; set; }
        public string? Status { get; set; }
        public DateTime DocumentDate { get; set; }
    }

    public class PR_Items : BaseRecord
    {
        public string RowID { get; set; }
        public string? UOM { get; set; }
        public string? Item { get; set; }
        public string Quantity { get; set; }
        public bool Selected { get; set; }
        public string? PRNumber { get; set; }
        public DateTime RequiredBy { get; set; }
    }

    public class PurchaseOrders : BaseRecord
    {
        public string? PurchaseOrderID { get; set; }
        public string? PurchaseOrderName { get; set; }
        public string? VendorID { get; set; }
        public string TotalAmount { get; set; }
        public string TotalQuantity { get; set; }
        public string? Status { get; set; }
        public string? RefrenceDocument { get; set; }
        public DateTime DocumentDate { get; set; }
    }

    public class PO_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string? PurchaseOrderID { get; set; }
        public string? Item { get; set; }
        public string RowID { get; set; }
        public string Quantity { get; set; }
        public string? UOM { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public DateTime RequiredBy { get; set; }
    }

    public class Vendor : BaseRecord
    {
        public string? VendorID { get; set; }
        public string? VendorName { get; set; }
        public string? VendorGroup { get; set; }
        public string? VendorType { get; set; }
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
    }

    public class RequestForQuotation : BaseRecord
    {
        public string? RFQNumber { get; set; }
        public string? RFQName { get; set; }
        public string? Status { get; set; }
        public DateTime DocumentDate { get; set; }
        public string? RefrenceDocument { get; set; }
    }

    public class RFQVendor : BaseRecord
    {
        public bool Selected { get; set; }
        public string? RFQNumber { get; set; }
        public string? VendorID { get; set; }
        public string RowID { get; set; }
        public bool SendEmail { get; set; }
    }

    public class RFQ_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string? RFQNumber { get; set; }
        public string? Item { get; set; }
        public string RowID { get; set; }
        public string Quantity { get; set; }
        public string? UOM { get; set; }
        public DateTime RequiredBy { get; set; }
    }

    public class GoodReceipt : BaseRecord
    {
        public string? GoodReceiptID { get; set; }
        public string? GoodReceiptName { get; set; }
        public string? Status { get; set; }
        public string? VendorID { get; set; }
        public string? RefrenceDocument { get; set; }
        public string TotalAmount { get; set; }
        public string TotalQuantity { get; set; }
        public DateTime DocumentDate { get; set; }
    }

    public class GR_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string? GoodReceiptID { get; set; }
        public string? Item { get; set; }
        public string RowID { get; set; }
        public string AcceptedQuantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }

    public class Quotation : BaseRecord
    {
        public string? QuotationID { get; set; }
        public string? QuotationName { get; set; }
        public string? Status { get; set; }
        public string? VendorID { get; set; }
        public string? RefrenceDocument { get; set; }
        public string TotalAmount { get; set; }
        public string TotalQuantity { get; set; }
        public DateTime DocumentDate { get; set; }
    }

    public class QU_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string? QuotationID { get; set; }
        public string? Item { get; set; }
        public string RowID { get; set; }
        public string Quantity { get; set; }
        public string? UOM { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }

    public class CancelAll : BaseRecord
    {
        public string? ID { get; set; }
        public string? type { get; set; }
    }

    public class PurchaseInvoice : BaseRecord
    {
        public string? PurchaseInvoiceID { get; set; }
        public string? PurchaseInvoiceName { get; set; }
        public string? Status { get; set; }
        public string? VendorID { get; set; }
        public string? RefrenceDocument { get; set; }
        public string TotalAmount { get; set; }
        public string TotalQuantity { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public bool IsReturn { get; set; }
        public string? VendorInvoiceNumber { get; set; }
        public DateTime? VendorInvoiceDate { get; set; }
    }

    public class PI_Items : BaseRecord
    {
        public bool Selected { get; set; }
        public string? PurchaseInvoiceID { get; set; }
        public string? Item { get; set; }
        public string RowID { get; set; }
        public string AcceptedQuantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }

    public class UnitofMeasure : BaseRecord
    {
        public string? UOMID { get; set; }
        public string? UOMName { get; set; }
        public string StockQuantity { get; set; }
        public string? Status { get; set; }
    }

    public class ItemClass : BaseRecord
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemType { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public string LeadTime { get; set; }
        public string? Status { get; set; }
        public bool Compatible { get; set; }
        public bool Alternate { get; set; }
    }

    public class ItemUOM : BaseRecord
    {
        public string? ItemID { get; set; }
        public string? UOMID { get; set; }
        public string RowID { get; set; }
        public bool Selected { get; set; }
    }

    public class Compatibility : BaseRecord
    {
        public string? ItemID { get; set; }
        public string? CompatibilityID { get; set; }
    }

    public class AlternateItem : BaseRecord
    {
        public string? AlternateID { get; set; }
        public string? ItemID { get; set; }
    }

    public class Stock : BaseRecord
    {
        public string? EntryID { get; set; }
        public string? ItemID { get; set; }
        public string? Type { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string RowID { get; set; }
        public bool Selected { get; set; }
        public string? Status { get; set; }
    }

    public class SendModel
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? RFQNumber { get; set; }
    }

    public class RFQItemReport
    {
        public string? No { get; set; }
        public string? RFQNumber { get; set; }
        public string? Item { get; set; }
        public string Quantity { get; set; }
        public string? UOM { get; set; }
        public string? RequiredBy { get; set; }
        public string? DocumentDate { get; set; }
    }

    public class PurchaseInvoiceReport
    {
        public string? Row { get; set; }
        public string? PurchaseInvoiceID { get; set; }
        public string? AcceptedQuantity { get; set; }
        public string? Rate { get; set; }
        public string? Amount { get; set; }
        public string? Item { get; set; }
        public string? Vendor { get; set; }
        public string? VendorAddress { get; set; }
        public string? VendorContact { get; set; }
        public string? VendorEmail { get; set; }
        public string? UOM { get; set; }
        public string? DueDate { get; set; }
        public string? TotalQuantity { get; set; }
        public string? TotalAmount { get; set; }
        public string? DocumentDate { get; set; }
        public string? RejectedQuantity { get; set; }
    }

    public class InventoryReport
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemType { get; set; }
        public string? Quantity { get; set; }
        public string? Rate { get; set; }
        public string? StockUpdated { get; set; }
        public string? TotalValue { get; set; }
        public string? ReportGenerated { get; set; }
    }

    public class PurchaseOrderAnalysis
    {
        public string? PurchaseOrderID { get; set; }
        public string? PurchaseOrderName { get; set; }
        public string? VendorName { get; set; }
        public string? TotalAmount { get; set; }
        public string? TotalQuantityOrdered { get; set; }
        public string? POStatus { get; set; }
        public string? DocumentDate { get; set; }
        public string? TotalBilledAmount { get; set; }
        public string? TotalAmountToBill { get; set; }
        public string? TotalQuantityReceived { get; set; }
        public string? TotalQuantityToReceive { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? ItemQuantity { get; set; }
        public string? UOMName { get; set; }
        public string? Rate { get; set; }
        public string? ItemAmount { get; set; }
    }
}