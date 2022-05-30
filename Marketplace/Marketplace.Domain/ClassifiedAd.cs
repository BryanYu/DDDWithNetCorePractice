﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Marketplace.Framework;
using Microsoft.VisualBasic.CompilerServices;

namespace Marketplace.Domain
{
    public class ClassifiedAd : AggregateRoot<ClassifiedAdId>
    {
        public UserId OwnerId { get; private set; }

        public ClassifiedAdId Id { get; private set; }

        public ClassifiedAdTitle Title { get; private set; }

        public ClassifiedAdText Text { get; private set; }

        public Money Price { get; private set; }

        public UserId ApprovedBy { get; private set; }

        public ClassifiedAdState State { get; private set; }

        public List<Picture> Pictures { get; private set; }


        public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
        {
            Pictures = new List<Picture>();
            Apply(new Events.ClassifiedAdCreated
            {
                Id = id,
                OwnerId = ownerId
            });
        }

        public void SetTitle(ClassifiedAdTitle title)
        {
            Apply(new Events.ClassifiedAdTitleChanged
            {
                Id = Id,
                Title = title
            });
        }


        public void UpdateText(ClassifiedAdText text)
        {
            Apply(new Events.ClassifiedAdTextUpdated
            {
                Id = Id,
                AdText = text,
            });
        }

        public void UpdatePrice(Price price)
        {
            Apply(new Events.ClassifiedAdPriceUpdated
            {
                Id = Id,
                Price = price.Amount,
                CurrencyCode = price.Currency.CurrencyCode
            });
        }

        public void RequestToPublish()
        {
            Apply(new Events.ClassifiedAdSentForReview
            {
                Id = Id
            });
        }

        public void AddPicture(Uri pictureUri, PictureSize size)
        {
            Apply(new Events.PictureAddedToAClassifiedAd
            {
                PictureId = new Guid(),
                ClassifiedAdId = Id,
                Url = pictureUri.ToString(),
                Height = size.Height,
                Width = size.Width,
                Order = Pictures.Max(o => o.Order) + 1
            });
        }

        public void ResizePicture(PictureId pictureId, PictureSize newSize)
        {
            var picture = FindPicture(pictureId);
            if (picture == null)
            {
                throw new InvalidOperationException("Cannot resize a picture that I don't have");
            }

            picture.Resize(newSize);
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ClassifiedAdCreated e:
                    Id = new ClassifiedAdId(e.Id);
                    OwnerId = new UserId(e.OwnerId);
                    State = ClassifiedAdState.Inactive;
                    break;
                case Events.ClassifiedAdTitleChanged e:
                    Title = new ClassifiedAdTitle(e.Title);
                    break;
                case Events.ClassifiedAdTextUpdated e:
                    Text = new ClassifiedAdText(e.AdText);
                    break;
                case Events.ClassifiedAdPriceUpdated e:
                    Price = new Price(e.Price, e.CurrencyCode);
                    break;
                case Events.ClassifiedAdSentForReview e:
                    State = ClassifiedAdState.PendingReview;
                    break;
                case Events.PictureAddedToAClassifiedAd e:
                    var picture = new Picture(Apply);
                    ApplyToEntity(picture, e);
                    Pictures.Add(picture);
                    break;
                    
            }
        }

        protected override void EnsureValidState()
        {
            var valid = Id != null && OwnerId != null &&
                        (State switch
                        {
                            ClassifiedAdState.PendingReview =>
                                Title != null &&
                                Text != null &&
                                Price?.Amount > 0 &&
                                FirstPicture.HasCorrectSize(),
                            ClassifiedAdState.Active =>
                                Title != null &&
                                Text != null &&
                                FirstPicture.HasCorrectSize() &&
                                ApprovedBy != null,
                            _ => true
                        });
            if (!valid)
            {
                throw new InvalidEntityStateException(this, $"Post-checks failed in state {State}");
            }
        }

        private Picture FindPicture(PictureId id)
        {
            return Pictures.FirstOrDefault(item => item.Id == id);
        }

        private Picture FirstPicture =>
            Pictures.OrderBy(item => item.Order).FirstOrDefault();
    }

    public enum ClassifiedAdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }

    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(object entity, string message) : base(
            $"entity {entity.GetType().Name} state change rejected, {message}")

        {

        }
    }
}
