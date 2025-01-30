using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zek.Persistence.Configurations
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<TProperty> HasColumnTypeDate<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("date");
        public static PropertyBuilder<TProperty> HasColumnTypeDateTime<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("datetime2(0)");
        public static PropertyBuilder<TProperty> HasColumnTypeTime<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("time(0)");

        public static PropertyBuilder<TProperty> HasColumnTypePercent<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(5, 2);
        public static PropertyBuilder<TProperty> HasColumnTypeFinPercent<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(7, 4);
        public static PropertyBuilder<TProperty> HasColumnTypeAmount<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(18, 2);
        public static PropertyBuilder<TProperty> HasColumnTypeFinAmount<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(18, 4);

        public static PropertyBuilder<TProperty> HasColumnTypeIpAddress<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(46);
        public static PropertyBuilder<TProperty> HasColumnTypePhoneNumber<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(50);
        public static PropertyBuilder<TProperty> HasColumnTypeEmail<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(320);
        public static PropertyBuilder<TProperty> HasColumnTypeCreditCard<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(25);
        public static PropertyBuilder<TProperty> HasColumnTypeTemperature<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(4, 1);
        public static PropertyBuilder<TProperty> HasColumnTypeWeight<TProperty>(this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(4, 1);
    }
}
