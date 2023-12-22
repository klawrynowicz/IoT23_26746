using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CdvAzure.Functions
{
    public class PeopleFn
    {
        private readonly ILogger _logger;

        private readonly PeopleService peopleService;

        public PeopleFn(ILoggerFactory loggerFactory, PeopleService peopleService)
        {
            _logger = loggerFactory.CreateLogger<PeopleFn>();
            this.peopleService = peopleService;
        }

        [Function("PeopleFn")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);   

            switch (req.Method)
            {
                case "POST":
                StreamReader reader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
                var json = reader.ReadToEnd();
                var person = JsonSerializer.Deserialize<Person>(json);
                var res = peopleService.Add(person.FirstName, person.LastName);
                response.WriteAsJsonAsync(res);
                break;
                case "PUT":
                StreamReader putReader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
                var putJson = putReader.ReadToEnd();
                var updatedPerson = JsonSerializer.Deserialize<Person>(putJson);
                var updated = peopleService.Update(updatedPerson.Id, updatedPerson.FirstName, updatedPerson.LastName);
                response.WriteAsJsonAsync(updated);
                break;
                case "GET":
                var people = peopleService.Get();
                response.WriteAsJsonAsync(people);
                break;
                case "DELETE":
                StreamReader deleteReader = new StreamReader(req.Body, System.Text.Encoding.UTF8);
                var deleteJson = deleteReader.ReadToEnd();
                var personToDelete = JsonSerializer.Deserialize<Person>(deleteJson);
                peopleService.Delete(personToDelete.Id);
                response.WriteString("Person deleted successfully");
                break;
            }


            return response;
        }
    }
}