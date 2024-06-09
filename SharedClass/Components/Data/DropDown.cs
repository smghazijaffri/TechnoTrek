using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace SharedClass.Components.Data
{
    public class DropDown
    {
        private int? openDropdownIndex = null;
        public List<option> Options { get; set; } = new List<option>();
        public List<BindDropdown> ListItems { get; set; } = new List<BindDropdown>();
        public void CloseDropdown(KeyboardEventArgs e, int rowIndex)
        {
            if (e.Key == "Escape")
            {
                ListItems[rowIndex].IsDropdownOpen = false;
            }
        }

        public void ToggleDropdown(int rowIndex, bool UOM = false)
        {
            if (openDropdownIndex.HasValue && openDropdownIndex != rowIndex)
            {
                ListItems[openDropdownIndex.Value].IsDropdownOpen = false;
            }

            ListItems[rowIndex].IsDropdownOpen = !ListItems[rowIndex].IsDropdownOpen;
            if (UOM == true)
            {
                openDropdownIndex = rowIndex;

            }
            else
            {
                if (ListItems[rowIndex].IsDropdownOpen)
                {
                    ListItems[rowIndex].FilteredOptions = Options;
                    openDropdownIndex = rowIndex;
                }
                else
                {
                    openDropdownIndex = null;
                }
            }
        }

        public bool FilterOptions(ChangeEventArgs e, int rowIndex)
        {
            var searchTerm = e.Value.ToString();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                ListItems[rowIndex].FilteredOptions = FilterList(Options, searchTerm);
                return true;
            }
            else
            {
                ListItems[rowIndex].FilteredOptions = Options;
                return false;
            }
        }

        public void HandleDropdownFocusOut(int rowIndex)
        {
            // Set IsDropdownOpen to false when the dropdown loses focus
            ListItems[rowIndex].IsDropdownOpen = false;
        }

        private List<option> FilterList(List<option> Options, string searchTerm)
        {
            List<option> filteredItems = new List<option>();

            foreach (var item in Options)
            {
                if (item.Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    filteredItems.Add(item);
                }
            }

            return filteredItems;
        }

        public bool SelectOption(option option, int rowIndex)
        {
            if (ListItems[rowIndex].FilteredOptions.Contains(option))
            {
                //ListItems[rowIndex].Item = option;
                ListItems[rowIndex].IsDropdownOpen = false;
            }
            return ListItems[rowIndex].IsDropdownOpen;
        }
    }

    public class BindDropdown
    {
        public bool IsDropdownOpen { get; set; }
        public List<option> FilteredOptions { get; set; } = new List<option>();
    }

    public class option
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }

    public class SingleDropDown
    {
        public List<option> Options { get; set; } = new List<option>();
        public List<BindDropdown> ListItems { get; set; } = new List<BindDropdown> { new BindDropdown { IsDropdownOpen = false } };
        public void CloseDropdown(KeyboardEventArgs e)
        {
            if (e.Key == "Escape")
            {
                ListItems[0].IsDropdownOpen = false;
            }
        }

        public void ToggleDropdown()
        {
            ListItems[0].IsDropdownOpen = !ListItems[0].IsDropdownOpen;

            if (ListItems[0].IsDropdownOpen)
            {
                ListItems[0].FilteredOptions = Options;
            }
        }

        public bool FilterOptions(ChangeEventArgs e)
        {
            var searchTerm = e.Value.ToString();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                ListItems[0].FilteredOptions = FilterList(Options, searchTerm);
                return true;
            }
            else
            {
                ListItems[0].FilteredOptions = Options;
                return false;
            }
        }

        private List<option> FilterList(List<option> Options, string searchTerm)
        {
            List<option> filteredItems = new List<option>();

            foreach (var item in Options)
            {
                if (item.Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    filteredItems.Add(item);
                }
            }

            return filteredItems;
        }

        public bool SelectOption(option option)
        {
            if (ListItems[0].FilteredOptions.Contains(option))
            {
                ListItems[0].IsDropdownOpen = false;
            }
            return ListItems[0].IsDropdownOpen;
        }
    }
}