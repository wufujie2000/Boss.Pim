using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;

namespace Boss.Pim.EntityFramework.Extensions
{
    public static class EntityTypeConfigurationExtensions
    {
        public static EntityTypeConfiguration<TEntityType> ToTableWithPrefix<TEntityType>(this EntityTypeConfiguration<TEntityType> configuration, string prefix = "")
            where TEntityType : class
        {
            return configuration.ToTable(prefix + typeof(TEntityType).Name.ToPluralize());
        }

        public static void ChangeTablePrefix(this DbModelBuilder modelBuilder, string prefix = "", params Type[] types)
        {
            BindingFlags flag = BindingFlags.Instance | BindingFlags.Public;
            var modelBuilderType = modelBuilder.GetType();
            MethodInfo method = modelBuilderType.GetMethod("Entity", flag);
            foreach (var type in types)
            {
                var typeMethod = method.MakeGenericMethod(type);
                dynamic config = typeMethod.Invoke(modelBuilder, new object[] { });
                config.ToTable(prefix + type.Name.ToPluralize());
            }
        }
    }
}
