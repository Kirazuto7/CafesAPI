using System;
namespace CafesAPI.Models
{
    public class Menu
    {
        public int MenuID { get; set; }

        public List<Item> Items { get; set; }
    }
}
