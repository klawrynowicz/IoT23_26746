namespace EfCoreReview.Rest.Models.Commands
{
    public class AddPersonCommand
    {
        public int AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}