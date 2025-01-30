namespace Zek.Model.DTO
{
    [Obsolete]
    public static class AddressExtensions
    {
        public static void Assign(this AddressDTO? model, Contact.Address? address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));

            if (model == null)
                return;


            address.CountryId = model.CountryId;
            address.City = model.City;
            address.Address1 = model.Address1;
            address.PostalCode = model.PostalCode;
        }
    }

    [Obsolete]
    public class AddressDTO
    {
        public int? Id { get; set; }

        /// <summary>
        /// Country ID (ISO 3166)
        /// </summary>
        public int? CountryId { get; set; }
        public string? Country { get; set; }

        public string? City { get; set; }


        public string? Address1 { get; set; }

        public string? PostalCode { get; set; }
    }
}
