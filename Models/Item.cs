using System;
using System.Text.Json.Serialization;
namespace CafesAPI.Models
{
    public class Item
    {
        [JsonIgnore]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
  
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        //[JsonIgnore]
        public int MenuId { get; set; }
        

    }
}
