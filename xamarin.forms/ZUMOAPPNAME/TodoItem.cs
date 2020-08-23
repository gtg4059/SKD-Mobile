using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace ZUMOAPPNAME
{
	public class TodoItem
	{
		

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
            get;
            set;
		}

		[JsonProperty(PropertyName = "dates")]
		public string dates
		{
            get;
            set;
		}

        [JsonProperty(PropertyName = "inout")]
        public string inout
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "names")]
        public string names
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "tags")]
        public string tags
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "gates")]
        public string gates
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "works")]
        public string works
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "location")]
        public string location
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "row")]
        public int row
        {
            get;
            set;
        }

        [Version]
        public string Version { get; set; }
	}
}

