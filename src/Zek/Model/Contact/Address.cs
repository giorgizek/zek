using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Contact
{
    public class Address
    {
        public int Id { get; set; }

        public int? CountryId { get; set; }
        public string City { get; set; }

        public string Address1 { get; set; }

        public string PostalCode { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }


    public class AddressMap : EntityTypeMap<Address>
    {
        public AddressMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Addresses", "Contact");
            HasKey(t => t.Id);
            
            Property(t => t.Id).ValueGeneratedOnAdd();
            Property(t => t.City).HasMaxLength(100);
            Property(t => t.Address1).HasMaxLength(200);
            Property(t => t.PostalCode).HasMaxLength(25);
            Property(t => t.Latitude).HasColumnType("decimal(10,6)");
            Property(t => t.Longitude).HasColumnType("decimal(10,6)");

            HasIndex(t => t.CountryId);
            HasIndex(t => t.City);
        }
    }
}