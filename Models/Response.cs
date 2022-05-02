using System;
namespace CafesAPI.Models
{
    public class Response
    {
        public int statusCode { get; set; }

        public string statusDescription { get; set; }

        public List<Cafe> cafes { get; set; } = new List<Cafe> { };

        public List<Menu> menus { get; set; } = new List<Menu> { };

        public List<Item> items { get; set; } = new List<Item> { };
    }
}
