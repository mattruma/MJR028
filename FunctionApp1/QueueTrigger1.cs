using FunctionApp1.Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public class QueueTrigger1
    {
        private readonly IHandler<QueueTrigger1Request, QueueTrigger1Response> _queueTrigger1Handler;

        public QueueTrigger1(
            IHandler<QueueTrigger1Request, QueueTrigger1Response> queueTrigger1Handler)
        {
            _queueTrigger1Handler = queueTrigger1Handler;
        }

        [FunctionName("QueueTrigger1")]
        public async Task Run(
            [QueueTrigger("queuetrigger1", Connection = "StorageConnectionString")]QueueTrigger1Request queueTrigger1Request,
            ILogger log,
            [Blob("queuetrigger1", FileAccess.Write, Connection = "StorageConnectionString")] CloudBlobContainer blobContainer)
        {
            log.LogInformation("QueueTrigger1 function processed a request.");

            try
            {
                var queueTrigger1Response =
                    _queueTrigger1Handler.Handle(
                        queueTrigger1Request);
            }
            catch (Exception ex)
            {
                var blobName =
                    $"{Guid.NewGuid()}-{DateTime.UtcNow.ToString().Replace(" ", "").Replace(":", "")}.json";

                var cloudBlockBlob =
                    blobContainer.GetBlockBlobReference(blobName);

                cloudBlockBlob.Properties.ContentType =
                    "application/json";

                await cloudBlockBlob.UploadTextAsync(
                    JsonConvert.SerializeObject(queueTrigger1Request, Formatting.Indented));

                throw ex;
            }
        }
    }
}
