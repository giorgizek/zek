namespace Zek.Model.DTO.Person
{
    public class PersonsDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PersonalNumber { get; set; }
        public int? GenderId { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone1 { get; set; }
        public string Fax1 { get; set; }
        public string Mobile1 { get; set; }
        public string Email1 { get; set; }
        public bool IsLegal { get; set; }
        public bool IsDeleted { get; set; }
    }
}
