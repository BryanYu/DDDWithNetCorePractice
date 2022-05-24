using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Marketplace.Api.Handler
{
    public class RetryPolicy
    {
        public RetryPolicy Retry { get; set; }

        public Task ExecuteAsync(Action action)
        {
            action.Invoke();
            return Task.CompletedTask;
        }
    }
 }
