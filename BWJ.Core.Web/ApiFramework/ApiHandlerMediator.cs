using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BWJ.Core.Web.ApiFramework
{
    public class ApiHandlerMediator : IApiHandlerMediator
    {
        private readonly IServiceProvider _services;

        public ApiHandlerMediator(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task<TResponse> Send<TResponse>(IApiHandlerContext<TResponse> context, ILogger logger)
            where TResponse : class
        {
            MethodGuard.NoNull(new { context });

            var contextType = context.GetType();
            var handlerType = typeof(ApiHandler<,>).MakeGenericType(contextType, typeof(TResponse));

            var handler = (IApiHandler<TResponse>)_services.GetRequiredService(handlerType);
            return await handler.HandleRequest(context, logger);
        }
    }
}
