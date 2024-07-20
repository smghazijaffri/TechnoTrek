using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Model
{
    public class PointofSale
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
    }

    public class SelectedItemsPOS
    {
        public string? CustomerID { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount => Quantity * Rate;
    }
}
