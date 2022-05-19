using System;
using System.Collections.Generic;
using System.Text;
using Marketplace.Domain;
using Xunit;

namespace Marketplace.Tests
{
    public class ClassifiedAdPublishSpec
    {
        private readonly ClassifiedAd _classifiedAd;

        public ClassifiedAdPublishSpec()
        {
            _classifiedAd = new ClassifiedAd(new ClassifiedAdId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()));
        }

        [Fact]
        public void Can_Publish_a_valid_ad()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100.10m, "EUR", new FakeCurrencyLookup()));
            _classifiedAd.RequestToPublish();

            Assert.Equal(ClassifiedAdState.PendingReview, _classifiedAd.State);
        }

        [Fact]
        public void Cannot_publish_without_title()
        {
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100.10m, "EUR", new FakeCurrencyLookup()));
            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_with_text()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(100.10m, "EUR", new FakeCurrencyLookup()));
            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_without_price()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }

        [Fact]
        public void Cannot_publish_with_zero_price()
        {
            _classifiedAd.SetTitle(ClassifiedAdTitle.FromString("Test ad"));
            _classifiedAd.UpdateText(ClassifiedAdText.FromString("Please buy my stuff"));
            _classifiedAd.UpdatePrice(Price.FromDecimal(0.0m, "EUR", new FakeCurrencyLookup()));
            Assert.Throws<InvalidEntityStateException>(() => _classifiedAd.RequestToPublish());
        }
    }
}
