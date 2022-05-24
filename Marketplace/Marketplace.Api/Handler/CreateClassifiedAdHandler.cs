using System.Threading.Tasks;
using Marketplace.Api.Contracts;
using Marketplace.Domain;

namespace Marketplace.Api.Handler
{
    public class CreateClassifiedAdHandler : IHandleCommand<Contracts.ClassifiedAds.V1.Create>
    {
        private readonly IEntityStore _store;

        public CreateClassifiedAdHandler(IEntityStore store)
        {
            _store = store;
        }

        public Task Handle(ClassifiedAds.V1.Create command)
        {
            var classified = new ClassifiedAd(new ClassifiedAdId(command.Id), new UserId(command.OwnerId));
            return _store.SaveAsync(classified);
        }
    }
}
