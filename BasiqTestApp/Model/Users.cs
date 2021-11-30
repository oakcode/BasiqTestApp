using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasiqTestApp.Model
{
    public class Users
    {
        [JsonProperty]
        public string type { get; set; }
        [JsonProperty]
        public int size { get; set; }
    }
}
