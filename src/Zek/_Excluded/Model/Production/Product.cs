using System;
using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;
using Zek.Model.Dictionary;

namespace Zek.Model.Production
{
    public class Product : BaseModel<int>
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Unique product identification number.
        /// </summary>
        public string ProductNumber { get; set; }
        /// <summary>
        /// 0 = Product is purchased, 1 = Product is manufactured in-house.
        /// </summary>
        public bool InHouse { get; set; }
        /// <summary>
        /// Minimum inventory quantity.
        /// </summary>
        public short SafetyStockLevel { get; set; }
        /// <summary>
        /// Inventory level that triggers a purchase order or work order.
        /// </summary>
        public short ReorderPoint { get; set; }
        /// <summary>
        /// Standard cost of the product.
        /// </summary>
        public decimal StandardCost { get; set; }
        /// <summary>
        /// Selling price.
        /// </summary>
        public decimal ListPrice { get; set; }

        public int CurrencyId { get; set; }

        /*
Size	nvarchar(5)	Checked
SizeUnitMeasureCode	nchar(3)	Checked
WeightUnitMeasureCode	nchar(3)	Checked
Weight	decimal(8, 2)	Checked
             */

        /// <summary>
        /// Number of days required to manufacture the product.
        /// </summary>
        public short DaysToManufacture { get; set; }

        /// <summary>
        /// Product is a member of this product subcategory (DD_Category.Id).
        /// </summary>
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        /// <summary>
        /// Product is a member of this product model (T_ProductModel).
        /// </summary>
        public int? ProductModelId { get; set; }

        /// <summary>
        /// Date the product was available for sale.
        /// </summary>
        public DateTime SellStartDate { get; set; }
        /// <summary>
        /// Date the product was no longer available for sale.
        /// </summary>
        public DateTime? SellEndDate { get; set; }
        /// <summary>
        /// Date the product was discontinued.
        /// </summary>
        public DateTime? DiscontinuedDate { get; set; }
    }


    public class ProductMap : BaseModelMap<Product, int>
    {
        public ProductMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Products", "Production");

            Property(t => t.Name).HasMaxLength(400).IsRequired();
            HasIndex(t => t.Name);

            Property(t => t.ProductNumber).HasMaxLength(25).IsRequired();
            HasIndex(t => t.ProductNumber);


            HasOne(t => t.Category).WithMany().HasForeignKey(t => t.CategoryId).OnDelete(DeleteBehavior.Restrict);
            //HasIndex(t => t.CategoryId);

            HasIndex(t => t.ProductModelId);

            Property(p => p.StandardCost).HasColumnType("decimal(18, 4)");
            Property(p => p.ListPrice).HasColumnType("decimal(18, 4)");
            Property(p => p.SellStartDate).HasColumnType("date");
            Property(p => p.SellEndDate).HasColumnType("date");
            Property(p => p.DiscontinuedDate).HasColumnType("date");
        }
    }
}
