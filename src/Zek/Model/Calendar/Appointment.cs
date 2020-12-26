using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Model.Base;

namespace Zek.Model.Calendar
{
    public class Appointment : PocoModel
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
    }

    public class AppointmentMap : PocoModelMap<Appointment>
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

            HasIndex(x => new { x.CreatorId, x.StartDate });
        }
    }
}
