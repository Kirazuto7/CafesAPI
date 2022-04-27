using System;
namespace CafesAPI.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public int Zipcode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        //public Cafe? Cafe { get; set; }

    }
}
