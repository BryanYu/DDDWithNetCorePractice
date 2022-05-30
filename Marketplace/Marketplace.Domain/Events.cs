﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public static class Events
    {
        public class ClassifiedAdCreated
        {
            public Guid Id { get; set; }

            public Guid OwnerId { get; set; }
        }

        public class ClassifiedAdTitleChanged
        {
            public Guid Id { get; set; }

            public string Title { get; set; }
        }

        public class ClassifiedAdTextUpdated
        {
            public Guid Id { get; set; }

            public string AdText { get; set; }
        }

        public class ClassifiedAdPriceUpdated
        {
            public Guid Id { get; set; }

            public decimal Price { get; set; }

            public string CurrencyCode { get; set; }
        }

        public class ClassifiedAdSentForReview
        {
            public Guid Id { get; set; }
        }

        public class PictureAddedToAClassifiedAd
        {
            public Guid PictureId { get; set; }

            public ClassifiedAdId ClassifiedAdId { get; set; }

            public string Url { get; set; }

            public int Height { get; set; }

            public int Width { get; set; }

            public int Order { get; set; }
        }

        public class ClassifiedAdPictureResized
        {
            public Guid PictureId { get; set; }

            public int Height { get; set; }

            public int Width { get; set; }
        }
    }
}
