using FunctionApp1.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class HttpTrigger1
    {
        private readonly IAsyncHandler<HttpTrigger1Request, HttpTrigger1Response> _httpTrigger1Handler;

        public HttpTrigger1(
            IAsyncHandler<HttpTrigger1Request, HttpTrigger1Response> httpTrigger1Handler)
        {
            _httpTrigger1Handler = httpTrigger1Handler;
        }

        [FunctionName("HttpTrigger1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log,
            [Blob("httptrigger1", FileAccess.Write, Connection = "StorageConnectionString")] CloudBlobContainer cloudBlobContainer,
            [Queue("queuetrigger1", Connection = "StorageConnectionString")] IAsyncCollector<QueueTrigger1Request> queueTrigger1RequestQueue)
        {
            log.LogInformation(
                "HttpTrigger1 function processed a request.");

            var httpTrigger1Request =
                JsonConvert.DeserializeObject<HttpTrigger1Request>(
                    await new StreamReader(req.Body).ReadToEndAsync());
            try
            {
                var httpTrigger1Response =
                    await _httpTrigger1Handler.HandleAsync(
                        httpTrigger1Request);

                return new OkObjectResult(
                    httpTrigger1Response);
            }
            catch (Exception ex)
            {
                var blobName =
                    $"{httpTrigger1Request.TransactionId}-{DateTime.UtcNow.ToString().Replace(" ", "").Replace(":", "")}.json";

                var cloudBlockBlob =
                    cloudBlobContainer.GetBlockBlobReference(blobName);

                cloudBlockBlob.Properties.ContentType =
                    "application/json";

                await cloudBlockBlob.UploadTextAsync(
                    JsonConvert.SerializeObject(httpTrigger1Request, Formatting.Indented));

                await queueTrigger1RequestQueue.AddAsync(
                    new QueueTrigger1Request
                    {
                        Recipients = new[] { "johndoe@somewhere.com" },
                        Subject = $"An error was encountered with {nameof(HttpTrigger1)}",
                        Body = ex.ToString()
                    });

                throw ex;
            }
        }
    }
}
