using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CdvAzure.Functions;
using Lab1.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => {
        services.AddDbContext<PeopleDb>(options=>{
            var connectionString = "Server=tcp:cdvdbserver1.database.windows.net,1433;Initial Catalog=cdvdbserver;Persist Security Info=False;User ID=kacper;Password=MySuperPassword12#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            options.UseSqlServer(connectionString);
        });
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton<PeopleService>();
    })
    .Build();

host.Run();