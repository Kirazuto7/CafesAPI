using System;
using System.Text.Json.Serialization;
namespace CafesAPI.Models
{
    public class Menu
    {

        public int MenuId { get; set; }
        
        public List<Item> Items { get; set; }
        //[JsonIgnore]
        public int CafeId { get; set; }
    }
}
