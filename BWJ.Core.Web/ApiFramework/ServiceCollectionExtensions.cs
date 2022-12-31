using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BWJ.Core.Web.ApiFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApiHandlers(this IServiceCollection services)
        {
            services.AddScoped<IApiHandlerMediator, ApiHandlerMediator>();

            var handlers = typeof(ServiceCollectionExtensions)
                .Assembly.GetExportedTypes()
                .Where(t => t.IsSubclassOfGenericClassDefinition(typeof(ApiHandler<,>)));

            foreach(var handler in handlers)
            {
                services.AddScoped(handler.BaseType, handler);
            }

            return services;
        }
    }
}
