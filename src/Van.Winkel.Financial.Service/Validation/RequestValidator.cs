using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Van.Winkel.Financial.Service.Validation
{
    public abstract class RequestValidator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : BaseResponse, new()
    {
        protected abstract Task<ValidationBag> Validate(TRequest request, CancellationToken cancellationToken);

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var bag = await Validate(request, cancellationToken);

            if (!bag.IsValid)
            {
                return new TResponse() { Bag = bag };
            }

            return await next();
        }
    }
}