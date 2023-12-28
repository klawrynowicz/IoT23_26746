
namespace EfCoreReview.Rest.Database.Entities
{
    public class AddressEntity
    {
        protected AddressEntity()
        {

        }
        public int Id { get; set; }
        public string AddressLine1 { get; protected set; }
        public string AddressLine2 { get; protected set; }
        public string City { get; protected set; }

        public List<PersonEntity> People { get; set; } = new List<PersonEntity>();

        public static AddressEntity Create(string addressLine1, string addressLine2, string city)
        {
            var entity = new AddressEntity
            {
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                City = city
            };

            return entity;
        }

        public PersonEntity AddPerson(string firstName, string lastName)
        {
            var personEntity = PersonEntity.Create(firstName, lastName);
            People.Add(personEntity);
            return personEntity;
        }
    }
}