using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zek.Persistence.Configurations
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<TProperty> HasColumnTypeDate<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("date");
        public static PropertyBuilder<TProperty> HasColumnTypeDateTime<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("datetime2(0)");
        public static PropertyBuilder<TProperty> HasColumnTypeTime<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("time(0)");

        public static PropertyBuilder<TProperty> HasColumnTypePercent<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(5, 2);
        public static PropertyBuilder<TProperty> HasColumnTypeFinPercent<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(7, 4);
        public static PropertyBuilder<TProperty> HasColumnTypeAmount<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(18, 2);
        public static PropertyBuilder<TProperty> HasColumnTypeFinAmount<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(18, 4);

        public static PropertyBuilder<TProperty> HasColumnTypeIpAddress<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(46);
        public static PropertyBuilder<TProperty> HasColumnTypePhoneNumber<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(50);
        public static PropertyBuilder<TProperty> HasColumnTypeEmail<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(320);
        public static PropertyBuilder<TProperty> HasColumnTypeCreditCard<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(25);
        public static PropertyBuilder<TProperty> HasColumnTypeTemperature<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(4, 1);
        public static PropertyBuilder<TProperty> HasColumnTypeWeight<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasPrecision(4, 1);
    }
}
