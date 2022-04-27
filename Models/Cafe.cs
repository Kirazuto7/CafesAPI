using System;
namespace CafesAPI.Models
{
    public class Cafe
    {
        public int CafeId { get; set; }
        public string CafeName { get; set; }
        public bool Wifi { get; set; }
        public int NumberOfTables   { get; set; }
        public int NumberOfOutlets  { get; set; }
        //public Menu? Menu { get; set; }
        public Location? Location { get; set; } 
        //public Schedule? Schedule { get; set; }
    }
}
