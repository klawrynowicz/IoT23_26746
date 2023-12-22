using Microsoft.EntityFrameworkCore;

namespace CdvAzure.Functions
{

    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:cdvdbserver1.database.windows.net,1433;Initial Catalog=cdvdbserver;Persist Security Info=False;User ID=kacper;Password=MySuperPassword12#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }

    public class PeopleService
{
    private readonly PersonContext _context;

    public PeopleService()
    {
        _context = new PersonContext();
    }

    public Person Add(string firstName, string lastName)
    {
        var person = new Person
        {
            FirstName = firstName,
            LastName = lastName
        };

        _context.People.Add(person);
        _context.SaveChanges();

        return person;
    }

    public Person Update(int id, string firstName, string lastName)
    {
        var person = _context.People.FirstOrDefault(w => w.Id == id);
        if (person != null)
        {
            person.FirstName = firstName;
            person.LastName = lastName;

            _context.SaveChanges();
        }

        return person;
    }

    public void Delete(int id)
    {
        var person = _context.People.FirstOrDefault(w => w.Id == id);
        if (person != null)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }
    }

    public Person Find(int id)
    {
        return _context.People.FirstOrDefault(w => w.Id == id);
    }

    public IEnumerable<Person> Get()
    {
        return _context.People.ToList();
    }
}

        public class Person
        {
            public int Id {get; set; }
            public string FirstName {get; set; }
            public string LastName { get; set; }
        }
}