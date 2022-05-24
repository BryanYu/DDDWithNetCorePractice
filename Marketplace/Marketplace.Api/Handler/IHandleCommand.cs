using System.Threading.Tasks;

namespace Marketplace.Api.Handler
{
    public interface IHandleCommand<in T>
    {
        Task Handle(T command);
    }
}
