namespace Zek.Office
{
    public class OnlineMeetingInfo
    {
        public string? ConferenceId { get; set; }
        public string? JoinUrl { get; set; }
        public IEnumerable<Phone>? Phones { get; set; }
        public string? QuickDial { get; set; }
        public IEnumerable<string>? TollFreeNumbers { get; set; }
        public string? TollNumber { get; set; }
    }
}