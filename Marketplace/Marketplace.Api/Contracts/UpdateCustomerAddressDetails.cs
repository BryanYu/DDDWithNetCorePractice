namespace Marketplace.Api.Contracts
{
    public class UpdateCustomerAddressDetails
    {
        public Address BillingAddress { get; set; }

        public Address DeliverAddress { get; set; }
    }
}
