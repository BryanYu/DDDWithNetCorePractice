using System.Threading.Tasks;
using Marketplace.Api.Handler;

namespace Marketplace.Api.Entity
{
    public class RavenDbEntityStore : IEntityStore
    {
        public Task<T> LoadAsync<T>(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync<T>(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
