namespace Zek.Office
{
    public class Location
    {
        public string DisplayName { get; set; }
        public PhysicalAddress Address { get; set; }
        public GeoCoordinates Coordinates { get; set; }
        /// <summary>
        /// The type of location. The possible values are: default, conferenceRoom, homeAddress, businessAddress,geoCoordinates, streetAddress, hotel, restaurant, localBusiness, postalAddress. Read-only.
        /// </summary>
        public int? LocationType { get; set; }

        public string LocationUri { get; set; }
        public string UniqueId { get; set; }
        public int? UniqueIdType { get; set; }
    }
}