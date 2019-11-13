using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;
using Zek.Model.Production;

namespace Zek.Model.Accounting
{
    public class OrderItem : BaseModel<int>
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Line { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? SpecialOfferId { get; set; }
        //todo  public SpecialOffer SpecialOffer { get; set; }

        public short Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal LineTotal { get; set; }

    }

    public class OrderItemMap : BaseModelMap<OrderItem, int>
    {
        public OrderItemMap(ModelBuilder builder) : base(builder)
        {
            ToTable("OrderItems", "Accounting");

            HasOne(o => o.Order).WithMany(o => o.Items).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Restrict);
            HasOne(o => o.Product).WithMany().HasForeignKey(o => o.ProductId).OnDelete(DeleteBehavior.Restrict);

            Property(o => o.Discount).HasColumnType("decimal(7, 4)");
            Property(o => o.LineTotal).HasComputedColumnSql("[UnitPrice]*Quantity*(1.0-[Discount])");
        }
    }
}
