using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;

namespace Zek.Model.Config
{
    public class Setting
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }

    public class SettingMap : EntityTypeMap<Setting>
    {
        public SettingMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Settings", "Config");
            HasKey(x => x.Id);
            Property(x => x.Id).HasMaxLength(255);
        }
    }
}