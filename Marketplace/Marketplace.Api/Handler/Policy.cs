namespace Marketplace.Api.Handler
{
    public class Policy
    {
        public static RetryPolicy Handle<T>()
        {
            return new RetryPolicy();
        }
    }
}
