using Newtonsoft.Json;
using System;

namespace FunctionApp1.Domain
{
    public class HttpTrigger1Response
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("transaction_id")]
        public Guid TransactionId { get; set; }

        [JsonProperty("job_id")]
        public Guid JobId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("received_on")]
        public DateTime? ReceivedOn { get; set; }

        [JsonProperty("processed_on")]
        public DateTime? ProcessedOn { get; set; }
    }
}
