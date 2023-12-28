using EfCoreReview.Rest.Database;
using EfCoreReview.Rest.Database.Entities;
using EfCoreReview.Rest.Models;
using EfCoreReview.Rest.Models.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreReview.Rest.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{

    private readonly ILogger<AddressController> _logger;
    private readonly DatabaseContext db;

    public AddressController(ILogger<AddressController> logger, DatabaseContext db)
    {
        _logger = logger;
        this.db = db;
    }

    [HttpGet("{id}", Name = "Find Address")]
    public IActionResult FindAddress([FromRoute] int id)
    {
        var addressEntity = db.Addresses
        //.Include(i => i.People)
        .FirstOrDefault(w => w.Id == id);

        if (addressEntity == null)
        {
            return NotFound();
        }


        return Ok(new Address
        {
            AddressLine1 = addressEntity.AddressLine1,
            AddressLine2 = addressEntity.AddressLine2,
            City = addressEntity.City,
            People = addressEntity.People.Select(s => new Person
            {
                FirstName = s.FirstName,
                LastName = s.LastName,
                Id = s.Id
            }).ToList()
        });
    }

    [HttpGet(Name = "Get Addresses")]
    public IActionResult CreateAddress()
    {
        var addresses = db.Addresses.ToList();
        return Ok(addresses);
    }

    [HttpPost(Name = "Create address")]
    public int CreateAddress([FromBody] CreateAddressCommand command)
    {
        var addressEntity = AddressEntity.Create(command.AddressLine1,
        command.AddressLine2,
        command.City);

        db.Addresses.Add(addressEntity);
        db.SaveChanges();
        return addressEntity.Id;
    }

    [HttpPost("{id}/person", Name = "Add person")]
    public IActionResult AddPerson([FromRoute] int id,
    [FromBody] AddPersonCommand command)
    {
        command.AddressId = id;
        var addressEntity = db.Addresses.FirstOrDefault(w => w.Id == command.AddressId);

        if (addressEntity == null)
        {
            return NotFound($"Address with id {command.AddressId} not found.");
        }

        addressEntity.AddPerson(command.FirstName, command.LastName);


        db.SaveChanges();
        return Ok();

    }
}
