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
        public bool IsDropdownOpen { get; set; }
        public DateTime? RequiredBy { get; set; }
        public List<string> FilteredOptions { get; set; }
    }
}