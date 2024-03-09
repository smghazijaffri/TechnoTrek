using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace SharedClass.Components.Data
{
    public class DropDown
    {
        private int? openDropdownIndex = null;
        public List<string> Options { get; set; }
        public List<BindDropdown> ListItems { get; set; } = new List<BindDropdown>();

        public void CloseDropdown(KeyboardEventArgs e, int rowIndex)
        {
            if (e.Key == "Escape")
            {
                ListItems[rowIndex].IsDropdownOpen = false;
            }
        }

        public void ToggleDropdown(int rowIndex)
        {
            if (openDropdownIndex.HasValue && openDropdownIndex != rowIndex)
            {
                ListItems[openDropdownIndex.Value].IsDropdownOpen = false;
            }

            ListItems[rowIndex].IsDropdownOpen = !ListItems[rowIndex].IsDropdownOpen;

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

        private List<string> FilterList(List<string> Options, string searchTerm)
        {
            List<string> filteredItems = new List<string>();

            foreach (string item in Options)
            {
                if (item.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    filteredItems.Add(item);
                }
            }

            return filteredItems;
        }

        public bool SelectOption(string option, int rowIndex)
        {
            if (ListItems[rowIndex].FilteredOptions.Contains(option))
            {
                ListItems[rowIndex].IsDropdownOpen = false;
            }
            return ListItems[rowIndex].IsDropdownOpen;
        }
    }

    public class BindDropdown
    {
        public int RowIndex { get; set; }
        public bool IsDropdownOpen { get; set; }
        public List<string> FilteredOptions { get; set; }
    }

    public class SingleDropDown
    {
        public List<string> Options { get; set; }
        public bool IsDropdownOpen { get; set; }
        public List<string> FilteredOptions { get; set; }

        public void CloseDropdown(KeyboardEventArgs e)
        {
            if (e.Key == "Escape")
            {
                IsDropdownOpen = false;
            }
        }

        public void ToggleDropdown()
        {
            IsDropdownOpen = !IsDropdownOpen;

            if (IsDropdownOpen)
            {
                FilteredOptions = Options;
            }
        }

        public bool FilterOptions(ChangeEventArgs e)
        {
            var searchTerm = e.Value.ToString();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                FilteredOptions = FilterList(Options, searchTerm);
                return true;
            }
            else
            {
                FilteredOptions = Options;
                return false;
            }
        }

        private List<string> FilterList(List<string> Options, string searchTerm)
        {
            List<string> filteredItems = new List<string>();

            foreach (string item in Options)
            {
                if (item.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    filteredItems.Add(item);
                }
            }

            return filteredItems;
        }

        public bool SelectOption(string option)
        {
            if (FilteredOptions.Contains(option))
            {
                IsDropdownOpen = false;
            }
            return IsDropdownOpen;
        }
    }
}