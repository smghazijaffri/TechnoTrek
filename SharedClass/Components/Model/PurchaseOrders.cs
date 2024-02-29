using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Model
{
    public class PurchaseOrders : BaseRecord
    {
        public bool Selected { get; set; }
        public string Vendor { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public DateTime RequiredBy { get; set; }
        public int RowID { get; set; }
        public string PONumber { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public string SupplierAddress { get; set; }
        public int SupplierContact { get; set; }
        public string CompanyShippingAddress { get; set; }
        public string CompanyBillingAddress { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
