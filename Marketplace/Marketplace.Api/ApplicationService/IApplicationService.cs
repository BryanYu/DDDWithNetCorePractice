using System.Threading.Tasks;

namespace Marketplace.Api.ApplicationService
{
    public interface IApplicationService
    {
        Task HandleAsync(object command);
    }
}
