using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marketplace.Domain
{
    public class FakeCurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<CurrencyDetails> _currencies = new CurrencyDetails[]
        {
            new CurrencyDetails
            {
                CurrencyCode = "EUR",
                DecimalPlaces = 2,
                InUse = true
            },
            new CurrencyDetails
            {
                CurrencyCode = "USD",
                DecimalPlaces = 2,
                InUse = true
            },
            new CurrencyDetails
            {
                CurrencyCode = "JPY",
                DecimalPlaces = 0,
                InUse = true
            },
            new CurrencyDetails
            {
                CurrencyCode = "DEM",
                DecimalPlaces = 2,
                InUse = false
            }
        };

        public CurrencyDetails FindCurrency(string currencyCode)
        {
            var currency = _currencies.FirstOrDefault(item => item.CurrencyCode == currencyCode);
            return currency ?? CurrencyDetails.None;
        }
    }
}
