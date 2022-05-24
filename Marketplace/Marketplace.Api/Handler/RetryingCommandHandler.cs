using System;
using System.Threading.Tasks;

namespace Marketplace.Api.Handler
{
    public class RetryingCommandHandler<T> : IHandleCommand<T>
    {
        private static RetryPolicy _policy = Policy.Handle<InvalidOperationException>().Retry;

        private IHandleCommand<T> _next;

        public RetryingCommandHandler(IHandleCommand<T> next)
        {
            _next = next;
        }

        public Task Handle(T command)
        {
            _policy.ExecuteAsync(() => _next.Handle(command));
            return Task.CompletedTask;
        }
    }
}
