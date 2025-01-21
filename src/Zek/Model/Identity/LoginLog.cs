﻿using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Persistence.Configurations;

namespace Zek.Model.Identity
{
    public class LoginLog
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
    }

    public class LoginLogMap : EntityTypeMap<LoginLog>
    {
        public LoginLogMap(ModelBuilder builder) : base(builder)
        {
            ToTable("LoginLogs", nameof(Schema.Identity));
            HasKey(x => x.Id);

            Property(x => x.Ip).HasMaxLength(46).IsRequired();
            Property(x => x.Date).HasColumnTypeDateTime();
        }
    }

    public class UserLoginLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string NormalizedUserName { get; set; }
        public string Ip { get; set; }
        public DateTime Date { get; set; }
    }

    public class UserLoginLogMap : EntityTypeMap<UserLoginLog>
    {
        public UserLoginLogMap(ModelBuilder builder) : base(builder)
        {
            ToTable("UserLoginLogs", nameof(Schema.Identity));
            HasIndex(x => x.Id);

            Property(u => u.NormalizedUserName).HasMaxLength(256);
            Property(x => x.Ip).HasMaxLength(46).IsRequired();
            Property(x => x.Date).HasColumnTypeDateTime();
        }
    }
}
