namespace EfCoreReview.Rest.Database.Entities
{
    public class PersonEntity
    {
        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }

        public static PersonEntity Create(string firstName, string lastName)
        {
            var entity = new PersonEntity
            {
                FirstName = firstName,
                LastName = lastName
            };

            return entity;
        }

        public int AddressId { get; set; }
        public AddressEntity Address { get; set; }
    }
}