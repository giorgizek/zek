namespace Zek.Model.DTO
{
    public class ContactBaseDTO
    {
        /// <summary>
        /// Contact ID
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Phone 1
        /// </summary>
        public string Phone1 { get; set; }

        /// <summary>
        /// Fax 1
        /// </summary>
        public string Fax1 { get; set; }

        /// <summary>
        /// Mobile 1
        /// </summary>
        public string Mobile1 { get; set; }

        /// <summary>
        /// Email 1
        /// </summary>
        public string Email1 { get; set; }
    }

    public class ContactDTO : ContactBaseDTO
    {
        /// <summary>
        /// Homepage or personal page
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Phone 2
        /// </summary>
        public string Phone2 { get; set; }

        /// <summary>
        /// Phone 3
        /// </summary>
        public string Phone3 { get; set; }

        /// <summary>
        /// Fax 2
        /// </summary>
        public string Fax2 { get; set; }

        /// <summary>
        /// Fax 3
        /// </summary>
        public string Fax3 { get; set; }

        /// <summary>
        /// Mobile 2
        /// </summary>
        public string Mobile2 { get; set; }

        /// <summary>
        /// Mobile 3
        /// </summary>
        public string Mobile3 { get; set; }

        /// <summary>
        /// Email 2
        /// </summary>
        public string Email2 { get; set; }

        /// <summary>
        /// Email 3
        /// </summary>
        public string Email3 { get; set; }
    }
}