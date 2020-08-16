using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Van.Winkel.Financial.Infrastructure.EntityFramework
{
    public static class ModelBuilderExtensions
    {
        public static void RegisteTypeConfigurations(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a =>
                a.GetTypes().Where(t =>
                    t.GetInterfaces()
                        .Where(i => i.IsGenericType)
                        .Select(i => i.GetGenericTypeDefinition())
                        .Contains(typeof(IEntityTypeConfiguration<>))));

            foreach (var type in types)
            {
                var tIEntityTypeConfiguration = type.GetInterfaces().Where(i => i.IsGenericType).First(i =>
                    i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

                var tIEntityTypeConfigurationGeneric = tIEntityTypeConfiguration.GetGenericArguments().First();

                var mApplyConfiguration = modelBuilder.GetType()
                    .GetMethods().First(m =>
                        m.Name == nameof(modelBuilder.ApplyConfiguration));

                var mApplyConfigurationGeneric =
                    mApplyConfiguration.MakeGenericMethod(tIEntityTypeConfigurationGeneric);
                var entityTypeConfiguration = Activator.CreateInstance(type);
                mApplyConfigurationGeneric.Invoke(modelBuilder, new[] { entityTypeConfiguration });
            }
        }
    }
}