using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Van.Winkel.Financial.Infrastructure.Mediatr
{
    public static class RequestValidatorConfig
    {
        public static IServiceCollection AddMediatRRequestValidators(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a =>
                a.GetTypes().Where(t =>
                {
                    if (t.IsAbstract)
                    {
                        return false;
                    }

                    var baseType = t.BaseType;
                    if (baseType == null || !baseType.IsGenericType)
                    {
                        return false;
                    }

                    var pipelineType = typeof(IPipelineBehavior<,>).MakeGenericType(baseType.GenericTypeArguments);

                    return pipelineType.IsAssignableFrom(t.BaseType);
                }));

            foreach (var type in types)
            {
                var pipelineType = typeof(IPipelineBehavior<,>).MakeGenericType(type.BaseType.GenericTypeArguments);
                services.AddTransient(pipelineType, type);
            }

            return services;
        }
    }
}
