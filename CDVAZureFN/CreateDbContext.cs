using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Lab1.Database;

public class DatabaseContextContextFactory : IDesignTimeDbContextFactory<PeopleDb>
{
    public PeopleDb CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PeopleDb>();
        optionsBuilder.UseSqlServer("Server=tcp:cdvbserver.database.windows.net,1433;Initial Catalog=cdvkamilbaza;Persist Security Info=False;User ID=kamil;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        return new PeopleDb(optionsBuilder.Options);
    }
}