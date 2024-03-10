using SharedClass.Components.Pages.AdminView.Buying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    //public class PurchaseOrders : BaseRecord
    //{
    //    public string PRNumbPurchaseOrderID { get; set; }
    //    public string PRnamePurchaseOrderName { get; set; }
    //    public string VendorID { get; set; }
    //    public int TotalAmount { get; set; }
    //    public int TotalQuanity { get; set; }
    //    public string Status { get; set; }
    //    public string RefrenceDocument { get; set; }
    //    public DateTime? DocumentDateDocumentDate { get; set; }

    //}
    public class PO_Items : BaseRecord
    {
        public string PRNumbPurchaseOrderID { get; set; }
        public string POItem { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public DateTime? RequiredBy { get; set; }

    }
}