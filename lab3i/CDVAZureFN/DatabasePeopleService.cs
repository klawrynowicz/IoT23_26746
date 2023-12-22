using CdvAzure.Functions;
using Lab1.Database;
//using Lab1.DTO;

namespace Lab1.Rest.Services{

    public class DatabasePeopleService : PeopleService{
        private readonly PeopleDb db;

        public DatabasePeopleService(PeopleDb db){
            this.db = db;
        }

        public IEnumerable<Person> GetPeople(){
            var peoplelist = db.People.Select(s=>new Person{
                FirstName = s.FirstName,
                Id =  s.Id,
                LastName = s.LastName
            });
            return peoplelist;
        }
    }
}