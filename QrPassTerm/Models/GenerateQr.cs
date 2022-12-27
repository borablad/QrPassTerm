using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace QrPassTerm.Models
{
    public class GenerateQr
    {
        [JsonProperty("data")]
        public int Code { get; set; }
    }
}
