using System;
using System.Threading.Tasks;
using Marketplace.Api.Contracts;
using Marketplace.Api.Handler;
using Marketplace.Api.Repository;
using Marketplace.Domain;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Marketplace.Api.ApplicationService
{
    public class ClassifiedAdsApplicationService : IApplicationService
    {

        private readonly IClassifiedAdRepository _repository;
        private ICurrencyLookup _currencyLookup;

        public ClassifiedAdsApplicationService(ICurrencyLookup currencyLookup, IClassifiedAdRepository repository)
        {
            _currencyLookup = currencyLookup;
            _repository = repository;
        }

        public Task HandleAsync(object command) =>
            command switch
            {
                ClassifiedAds.V1.Create cmd => HandleCreateAsync(cmd),
                ClassifiedAds.V1.SetTitle cmd => HandleUpdateAsync(cmd.Id,
                    c => c.SetTitle(ClassifiedAdTitle.FromString(cmd.Title))),
                ClassifiedAds.V1.UpdateText cmd =>
                    HandleUpdateAsync(cmd.Id,
                        c => c.UpdateText(ClassifiedAdText.FromString(cmd.Text))),
                ClassifiedAds.V1.UpdatePrice cmd => HandleUpdateAsync(cmd.Id,
                    c => c.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency, _currencyLookup))),
                ClassifiedAds.V1.RequestToPublish cmd => HandleUpdateAsync(cmd.Id,
                    c => c.RequestToPublish()),
                _ => Task.CompletedTask
            };

        //switch (command)
        //{
        //    case ClassifiedAds.V1.Create cmd:
        //        await HandleCreateAsync(cmd);
        //        break;
        //    case ClassifiedAds.V1.SetTitle cmd:
        //        await HandleUpdateAsync(cmd.Id, c => c.SetTitle(ClassifiedAdTitle.FromString(cmd.Title)));
        //        break;
        //    case ClassifiedAds.V1.UpdateText cmd:
        //        await HandleUpdateAsync(cmd.Id, c => c.UpdateText(ClassifiedAdText.FromString(cmd.Text)));
        //        break;
        //    case ClassifiedAds.V1.UpdatePrice cmd:
        //        await HandleUpdateAsync(cmd.Id,
        //             c => c.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency, _currencyLookup)));
        //        break;
        //    case ClassifiedAds.V1.RequestToPublish cmd:
        //        await HandleUpdateAsync(cmd.Id, c => c.RequestToPublish());
        //        break;
        //    default:
        //        throw new InvalidOperationException($"Command type {command.GetType().FullName} is unknown");
        //} 


        public async Task HandleCreateAsync(ClassifiedAds.V1.Create cmd)
        {
            if (await _repository.ExistsAsync(cmd.Id.ToString()))
            {
                throw new InvalidOperationException($"Entity with id {cmd.Id} already exists");
            }

            var classifiedAd = new ClassifiedAd(new ClassifiedAdId(cmd.Id), new UserId(cmd.OwnerId));
            await _repository.SaveAsync(classifiedAd);
        }

        public async Task HandleUpdateAsync(Guid classifiedAdId, Action<ClassifiedAd> operation)
        {
            var classifiedAd = await _repository.LoadAsync<ClassifiedAd>(classifiedAdId.ToString());
            if (classifiedAd == null)
            {
                throw new InvalidOperationException($"Entity with id {classifiedAdId} cannot be found");
            }

            operation(classifiedAd);
            await _repository.SaveAsync(classifiedAd);
        }
    }
}

