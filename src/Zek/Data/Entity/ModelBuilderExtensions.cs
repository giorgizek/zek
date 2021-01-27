using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Zek.Data.Entity
{
    public static class ModelBuilderExtensions
    {
        public static void InitConventions(this ModelBuilder builder)
        {
            foreach (var type in builder.Model.GetEntityTypes())
            {
                var schema = type.GetSchema();
                var tableName = type.GetTableName();

                //foreach (var key in type.GetKeys())
                //{
                //    key.SetName("PK_" + tableName);
                //}

                foreach (var index in type.GetIndexes())
                {
                    if (!index.IsUnique) continue;

                    var name = "AK_" + tableName + "_" + string.Join("_", index.Properties.Select(p => p.Name));
                    //var name = (index.IsUnique ? "AK_" : "IX_") + tableName + "_" + string.Join("_", index.Properties.Select(p => p.Name));
                    index.SetDatabaseName(name);
                }

                //foreach (var fk in type.GetForeignKeys())
                //{
                //    var masterTable = fk.DependentToPrincipal.GetTargetType().GetTableName();
                //    var name = "FK_" + tableName + "_" + masterTable + "_" + string.Join("_", fk.Properties.Select(p => p.Name));
                //    fk.SetConstraintName(name);
                //}
            }
        }
    }
}
