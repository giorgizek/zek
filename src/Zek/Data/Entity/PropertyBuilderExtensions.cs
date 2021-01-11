using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Zek.Data.Entity
{
    public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<TProperty> HasColumnTypeDate<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("date");
        public static PropertyBuilder<TProperty> HasColumnTypeDateTime<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("datetime2(0)");

        public static PropertyBuilder<TProperty> HasColumnTypePhoneNumber<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasMaxLength(50);

        public static PropertyBuilder<TProperty> HasColumnTypeTemperature<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("decimal(4,1)");
        public static PropertyBuilder<TProperty> HasColumnTypeWeight<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("decimal(4,1)");

        public static PropertyBuilder<TProperty> HasColumnTypePercent<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("decimal(5,2)");
        public static PropertyBuilder<TProperty> HasColumnTypeFinPercent<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("decimal(7,4)");
        public static PropertyBuilder<TProperty> HasColumnTypeAmount<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("decimal(18,2)");
        public static PropertyBuilder<TProperty> HasColumnTypeFinAmount<TProperty>([NotNull] this PropertyBuilder<TProperty> propertyBuilder) => propertyBuilder.HasColumnType("decimal(18,4)");
    }
}
