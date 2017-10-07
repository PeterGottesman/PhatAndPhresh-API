using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhatAndPhresh.Models
{
    public class WordResponse
    {
		[JsonProperty]
		public string Word { get; set; }

		[JsonProperty]
		public int Score { get; set; }

        [JsonProperty]
        public string NumSyllables { get; set; }

		[JsonProperty]
		public List<string> tags { get; set; }
    }
}
