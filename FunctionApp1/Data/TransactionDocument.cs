using Newtonsoft.Json;
using System;

namespace FunctionApp1.Data
{
    public class TransactionDocument
    {
        public const string STATUS_PENDING = "Pending";
        public const string STATUS_PROCESSING = "Processing";
        public const string STATUS_DONE = "Done";

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("transaction_id")]
        public Guid TransactionId { get; set; }

        [JsonProperty("job_id")]
        public Guid JobId => this.Id;

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("received_on")]
        public DateTime ReceivedOn { get; set; }

        [JsonProperty("processed_on")]
        public DateTime? ProcessedOn { get; set; }

        public TransactionDocument()
        {
            this.Id = Guid.NewGuid();
            this.Status = STATUS_PENDING;
            this.ReceivedOn = DateTime.UtcNow;
        }
    }
}
