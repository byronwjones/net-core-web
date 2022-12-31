using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BWJ.Core.Web.ApiFramework
{
    public abstract class ApiHandlerMvcControllerBase<TController> : ControllerBase
    {
        private readonly ILogger<TController> _logger;
        private readonly IApiHandlerMediator _mediator;

        public ApiHandlerMvcControllerBase(
            ILogger<TController> logger,
            IApiHandlerMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected async Task<TResponse> HandleRequest<TResponse>(IApiHandlerContext<TResponse> handlerContext)
            where TResponse : class 
            => await _mediator.Send(handlerContext, _logger);
    }
}
