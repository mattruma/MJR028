using Newtonsoft.Json;
using System;

namespace FunctionApp1.Domain
{
    public class HttpTrigger1Request
    {
        [JsonProperty("transaction_id")]
        public Guid TransactionId { get; set; }
    }
}
