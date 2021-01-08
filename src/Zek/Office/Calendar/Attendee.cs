namespace Zek.Office
{
    public class Attendee : Recipient
    {
        public string Id { get; set; }
        public AttendeeType? Type { get; set; }
        public ResponseStatus Status { get; set; }
    }
}