using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClass.Components.Model
{
    public class SimpleProductModel
    {
        public ProductModel ProductModel { get; set; }
        public string avatarName { get; set; }
        public List<int> ProductLocation { get; set; }
        public ProductVariants productVariants { get; set; }
        public List<int> productTax { get; set; }
        public List<ProductVariantsPrice> productVariantsPrice { get; set; }
        public List<SimpleProductStock> simpleProductStocks { get; set; }
    }
    public class ProductModel : BaseRecordPOS
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReceiptName { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public int CategoryId { get; set; }
        public int SubCatId { get; set; }
        public int ItemType { get; set; }
        public string ImageName { get; set; }
        public string ErpProductID { get; set; }
    }
    public class ProductVariantsPrice : BaseRecordPOS
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal MarkUp { get; set; }
        public int LocationId { get; set; }
        public bool IsOverride { get; set; }
    }
    public class SimpleProductStock
    {
        public ProductVariantsStockHistory ProductVariantsStockHistory { get; set; }
        public ProductVariantsStock ProductVariantsStock { get; set; }
        public int QtyChange { get; set; }
    }
    public class ProductVariantsStockHistory : BaseRecordPOS
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public int StockAction { get; set; }
        public decimal ActionQty { get; set; }
        public int LocationId { get; set; }
    }
    public class ProductVariantsStock : BaseRecordPOS
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VariantId { get; set; }
        public decimal CurrentStockQty { get; set; }
        public bool StockTracking { get; set; }
        public decimal LowStockAlertQty { get; set; }
        public bool LowStockAlert { get; set; }
        public int LocationId { get; set; }
    }
    public class ProductVariants : BaseRecordPOS
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string AdditionalCode { get; set; }
    }
    public class ProductLocation : BaseRecordPOS
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
    }
    public class ProductTax : BaseRecordPOS
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TaxId { get; set; }
    }


}
