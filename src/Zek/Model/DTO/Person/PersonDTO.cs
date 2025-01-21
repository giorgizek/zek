using Zek.Model.Dictionary;

namespace Zek.Model.DTO.Person
{
    public class PersonDTO : EditBaseDTO
    {
        public bool? IsLegal { get; set; }

        public byte? TitleId { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string FirstNameEn { get; set; }

        public string LastNameEn { get; set; }

        public string FullNameEn { get; set; }

        public string PersonalNumber { get; set; }

        public string Passport { get; set; }

        public Gender? GenderId { get; set; }

        public MaritalStatus? MaritalStatusId { get; set; }

        public DateTime? BirthDate { get; set; }

        public int? ResidentId { get; set; }

        public AddressDTO Address { get; set; }

        public ContactDTO Contact { get; set; }
    }
}
