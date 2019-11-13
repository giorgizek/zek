using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Contact
{
    public class Contact
    {
        public int Id { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string Fax3 { get; set; }

        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }

        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        public string Url  { get; set; }
    }


    public class ContactMap : EntityTypeMap<Contact>
    {
        public ContactMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Contacts", "Contact");
            HasKey(t => t.Id);
            Property(t => t.Id).ValueGeneratedOnAdd();

            Property(t => t.Phone1).HasMaxLength(25);
            Property(t => t.Phone2).HasMaxLength(25);
            Property(t => t.Phone3).HasMaxLength(25);

            Property(t => t.Fax1).HasMaxLength(25);
            Property(t => t.Fax2).HasMaxLength(25);
            Property(t => t.Fax3).HasMaxLength(25);

            Property(t => t.Mobile1).HasMaxLength(25);
            Property(t => t.Mobile2).HasMaxLength(25);
            Property(t => t.Mobile3).HasMaxLength(25);

            Property(t => t.Email1).HasMaxLength(256);
            Property(t => t.Email2).HasMaxLength(256);
            Property(t => t.Email3).HasMaxLength(256);

            Property(t => t.Url).HasMaxLength(2083);

            HasIndex(t => t.Email1);
            HasIndex(t => t.Phone1);
            HasIndex(t => t.Mobile1);
        }
    }
}
