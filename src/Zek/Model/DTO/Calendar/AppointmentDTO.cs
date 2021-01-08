using System;

namespace Zek.Model.DTO.Calendar
{
    public class AppointmentDTO<TType, TStatus, TLabel>
        where TType : struct, IConvertible
        where TStatus : struct, IConvertible
        where TLabel : struct, IConvertible
    {
        public string AppointmentNumber { get; set; }
        public TType? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public TLabel? Label { get; set; }
    }
}
