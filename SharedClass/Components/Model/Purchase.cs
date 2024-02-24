﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Model
{
    public class Purchase
    {
        public bool Selected { get; set; }
        public int PRNumber { get; set; }
        public string Vendor { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public DateTime RequiredBy { get; set; }
        public int ForInsert { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
