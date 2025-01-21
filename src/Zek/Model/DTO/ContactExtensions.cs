namespace Zek.Model.DTO
{
    public static class ContactExtensions
    {
        public static void Assign(this ContactDTO model, Model.Contact.Contact contact)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            if (model == null)
                return;

            contact.Mobile1 = model.Mobile1;
            contact.Mobile2 = model.Mobile2;
            contact.Mobile3 = model.Mobile3;
            contact.Phone1 = model.Phone1;
            contact.Phone2 = model.Phone2;
            contact.Phone3 = model.Phone3;
            contact.Fax1 = model.Fax1;
            contact.Fax2 = model.Fax2;
            contact.Fax3 = model.Fax3;
            contact.Email1 = model.Email1;
            contact.Email2 = model.Email2;
            contact.Email3 = model.Email3;
            contact.Url = model.Url;
        }
    }
}