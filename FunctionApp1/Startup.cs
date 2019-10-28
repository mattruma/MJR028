using FunctionApp1.Data;
using FunctionApp1.Domain;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(FunctionApp1.Startup))]
namespace FunctionApp1
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(
            IFunctionsHostBuilder builder)
        {
            var cosmosClient =
                new CosmosClientBuilder(Environment.GetEnvironmentVariable("CosmosDBConnectionString"))
                    .WithConnectionModeDirect()
                    .Build();

            builder.Services.AddSingleton(cosmosClient);

            builder.Services.AddTransient<TransactionDocumentStore, TransactionDocumentStore>();

            builder.Services.AddTransient<IAsyncHandler<HttpTrigger1Request, HttpTrigger1Response>, HttpTrigger1Handler>();
            builder.Services.AddTransient<IHandler<QueueTrigger1Request, QueueTrigger1Response>, QueueTrigger1Handler>();
        }
    }
}
