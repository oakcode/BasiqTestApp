using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasiqTestApp.Model
{
    public class Transactions
    {
        [JsonProperty]
        public List<Transaction> data { get; set; }
        [JsonProperty]
        public int size { get; set; }
        [JsonProperty]
        public Links links { get; set; }
    }


    public class Transaction
    {
        [JsonProperty]
        public float amount { get; set; }
        [JsonProperty]
        public SubClass subClass { get; set; }

        //type and detail will be populated only in situation when something goes wrong
        [JsonProperty]
        public string type { get; set; }
        [JsonProperty]
        public string detail { get; set; }
    }

    public class SubClass
    {
        [JsonProperty]
        public int code { get; set; }
        [JsonProperty]
        public string title { get; set; }
    }

    public class Links
    {
        [JsonProperty]
        public string next { get; set; }
    }
}
