using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateMatcher.Loader
{
    class RuleCollectionModel
    {
        [JsonProperty("namespace")]
        public string Namepsace { get; set; }

        [JsonProperty("meta")]
        public string Meta { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }
    }
    class RuleModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("extends")]
        public string Extends { get; set; }

        [JsonProperty("criteria")]
        public string[] Criteria { get; set; }

        [JsonProperty("texts")]
        public string[] Text { get; set; }

        [JsonProperty("scripts")]
        public string[] Scripts { get; set; }

    }
}
