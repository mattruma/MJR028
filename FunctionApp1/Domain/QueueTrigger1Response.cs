using Newtonsoft.Json;
using System;

namespace FunctionApp1.Domain
{
    public class QueueTrigger1Response : QueueTrigger1Request
    {

        [JsonProperty("sent_on")]
        public DateTime? SentOn { get; set; }

        public QueueTrigger1Response()
        {
            this.SentOn = DateTime.UtcNow;
        }
    }
}
