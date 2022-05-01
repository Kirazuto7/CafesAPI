using System;
namespace CafesAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }

        public string statusDescription { get; set; }

        public List<Cafe?> cafes { get; set; }
    }
}
