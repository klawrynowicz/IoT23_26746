namespace EfCoreReview.Rest.Models.Commands
{
    public class CreateAddressCommand
    {
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}