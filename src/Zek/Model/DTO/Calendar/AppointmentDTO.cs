using System;

namespace Zek.Model.DTO.Calendar
{
    public class AppointmentDTO: AppointmentDTO<int, int, int>
    {
    }
    
    public class AppointmentDTO<TType, TStatus, TLabel>
        where TType : struct, IConvertible
        where TStatus : struct, IConvertible
        where TLabel : struct, IConvertible
    {
        public int? Id { get; set; }
        public string AppointmentNumber { get; set; }
        public TType? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? AllDay { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public TStatus? Status { get; set; }
        public TLabel? Label { get; set; }
        public int? ResourceId { get; set; }
        public string ResourceIds { get; set; }
        public string ReminderInfo { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
