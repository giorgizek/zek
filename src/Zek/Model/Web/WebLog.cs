using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Web
{
    public class WebLog
    {
        public int Id { get; set; }
        public DateTime RequestTimestamp { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public string RequestContentType { get; set; }
        //public string RequestHeaders { get; set; }
        public string RequestBody { get;set; }

        public DateTime? ResponseTimestamp { get; set; }
        public string ResponseContentType { get; set; }
        public int? ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; }
    }

    public class WebLogMap : EntityTypeMap<WebLog>
    {
        public WebLogMap(ModelBuilder builder) : base(builder)
        {
            ToTable("WebLogs", "Log");
            HasKey(x => x.Id);

            Property(x => x.Id).ValueGeneratedOnAdd();
            Property(x => x.RequestContentType).HasMaxLength(100);
            Property(x => x.RequestUri).HasMaxLength(2083);
            Property(x => x.RequestMethod).HasMaxLength(100);
            Property(x => x.RequestTimestamp).HasColumnType("datetime2(3)");
            Property(x => x.ResponseContentType).HasMaxLength(100);
            Property(x => x.ResponseTimestamp).HasColumnType("datetime2(3)");
        }
    }
}
