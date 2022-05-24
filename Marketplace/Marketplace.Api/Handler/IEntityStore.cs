using System.Threading.Tasks;

namespace Marketplace.Api.Handler
{
    public interface IEntityStore
    {
        Task<T> LoadAsync<T>(string id);
        Task SaveAsync<T>(T entity);

        Task<bool> ExistsAsync(string id);
    }
}
