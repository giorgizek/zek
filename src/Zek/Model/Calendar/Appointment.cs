using Microsoft.EntityFrameworkCore;
using Zek.Domain.Entities;
using Zek.Model.Base;
using Zek.Persistence.Configurations;

namespace Zek.Model.Calendar
{
    public class Appointment : PocoEntity
    {
        public int Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? AllDay { get; set; }
        public string Subject { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int Label { get; set; }
        public int? ResourceId { get; set; }
        public string ResourceIds { get; set; }
        public string ReminderInfo { get; set; }
        public string RecurrenceInfo { get; set; }
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }


    public class AppointmentMap : AppointmentMap<Appointment>
    {
        public AppointmentMap(ModelBuilder builder) : base(builder)
        {
        }
    }

    public class AppointmentMap<TAppointment> : PocoModelMap<TAppointment>
        where TAppointment : Appointment
    {
        public AppointmentMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Appointments", "Calendar");
            Property(x => x.StartDate).HasColumnTypeDateTime();
            Property(x => x.EndDate).HasColumnTypeDateTime();
            Property(x => x.Subject).HasMaxLength(200);
            Property(x => x.Location).HasMaxLength(2083);
            Property(x => x.Description).HasMaxLength(4000);
            Property(x => x.ResourceIds).HasMaxLength(4000);
            Property(x => x.ReminderInfo).HasMaxLength(4000);
            Property(x => x.RecurrenceInfo).HasMaxLength(4000);
            Property(u => u.ConcurrencyStamp).HasMaxLength(50).IsConcurrencyToken();

            HasIndex(x => new { x.CreatorId, x.StartDate });
        }
    }
}
