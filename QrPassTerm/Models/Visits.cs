using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrPassTerm.Models
{
    public class Visits
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("visitdate")]
        public DateTime VisitDate { get; set; }
        [JsonProperty("lte")]
        public string lte { get; set; }
    }
}
