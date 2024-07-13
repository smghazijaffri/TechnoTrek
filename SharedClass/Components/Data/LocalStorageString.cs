using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Data
{
    public class LocalStorageString
    {
        private string _myString;

        // Public property to access and set the string value
        public string MyString
        {
            get { return _myString; }
            set { _myString = value; }
        }

        // Constructor to initialize the string value (optional)
        public LocalStorageString(string initialValue)
        {
            _myString = initialValue;
        }
    }
}
