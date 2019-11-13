using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Contact
{
    public class Map
    {
        public int Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }

    public class MapMap : EntityTypeMap<Map>
    {
        public MapMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Maps", "Contact");
            HasKey(t => t.Id);
            Property(t => t.Id).ValueGeneratedOnAdd();

            Property(t => t.Latitude).HasColumnType("decimal(10,6)");
            Property(t => t.Longitude).HasColumnType("decimal(10,6)");
        }
    }
}
