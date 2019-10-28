using FunctionApp1.Data;
using System.Threading.Tasks;

namespace FunctionApp1.Domain
{
    public class HttpTrigger1Handler : IAsyncHandler<HttpTrigger1Request, HttpTrigger1Response>
    {
        private readonly TransactionDocumentStore _transactionDocumentStore;

        public HttpTrigger1Handler(
            TransactionDocumentStore transactionDocumentStore)
        {
            _transactionDocumentStore = transactionDocumentStore;
        }

        public async Task<HttpTrigger1Response> HandleAsync(
            HttpTrigger1Request httpTrigger1Request)
        {
            var transactionDocument =
                new TransactionDocument
                {
                    TransactionId = httpTrigger1Request.TransactionId
                };

            transactionDocument =
                await _transactionDocumentStore.AddAsync(
                    transactionDocument);

            var httpTrigger1Response =
                new HttpTrigger1Response
                {
                    Id = transactionDocument.Id,
                    TransactionId = transactionDocument.TransactionId,
                    JobId = transactionDocument.JobId,
                    Status = transactionDocument.Status,
                    ReceivedOn = transactionDocument.ReceivedOn,
                    ProcessedOn = transactionDocument.ProcessedOn
                };

            return httpTrigger1Response;
        }
    }
}
