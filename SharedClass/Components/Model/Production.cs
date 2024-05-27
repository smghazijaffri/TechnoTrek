namespace SharedClass.Components.Model
{
    public class BOM : BaseRecord
    {
        public string BOMID { get; set; }
        public string BOMName { get; set; }
        public string ItemName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime DocumentDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class BOM_Item : BaseRecord
    {
        public bool Selected { get; set; }
        public string BOMID { get; set; }
        public string Item { get; set; }
        public string UOM { get; set; }
        public int RowID { get; set; }
        public int Quantity { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
    }

}
