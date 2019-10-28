using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionApp1.Domain
{
    public class QueueTrigger1Request
    {
        [JsonProperty("recipients")]
        public IEnumerable<string> Recipients { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }
}
