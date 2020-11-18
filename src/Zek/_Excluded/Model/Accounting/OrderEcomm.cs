using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Accounting
{
    public class OrderEcomm
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int EcommId { get; set; }
        public Ecomm Ecomm { get; set; }
    }

    public class OrderEcommMap : EntityTypeMap<OrderEcomm>
    {
        public OrderEcommMap(ModelBuilder builder) : base(builder)
        {
            ToTable("OrderEcomms", "Accounting");
            HasKey(t => t.OrderId);

            HasOne(t => t.Order).WithOne().HasForeignKey<OrderEcomm>(t => t.OrderId).OnDelete(DeleteBehavior.Restrict);

            HasOne(t => t.Ecomm).WithOne().HasForeignKey<OrderEcomm>(t => t.EcommId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
