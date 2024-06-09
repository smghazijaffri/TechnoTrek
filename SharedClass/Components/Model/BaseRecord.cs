using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Model
{
    public class BaseRecord
    {
        public int ForInsert { get; set; }
        public DateTime CreationDate { get; set; }
        public string Output { get; set; }
    }
    public class OutputClass
    {
        public string Output { get; set; }
        public string ErrorMessage { get; set; }
    }
}
