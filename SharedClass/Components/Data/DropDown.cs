using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Options;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int RowIndex { get; set; }
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