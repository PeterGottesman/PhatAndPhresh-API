using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PhatAndPhresh.Models
{
    public class Rap
    {
		[JsonProperty]
		public List<string> Verses { get; set; }

		[JsonProperty]
		public List<string> Rhymes { get; set; }
    }
}
