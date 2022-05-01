using System;
using System.Text.Json.Serialization;
namespace CafesAPI.Models
{
    public class Location
    {
        [JsonIgnore]
        public int LocationId { get; set; }
        public int Zipcode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        //[JsonIgnore]
        public int CafeId { get; set; }
    }
}
