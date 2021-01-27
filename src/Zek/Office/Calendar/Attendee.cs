namespace Zek.Office
{
    public class Attendee : Recipient
    {
        public AttendeeType? Type { get; set; }
        public ResponseStatus Status { get; set; }
    }
}