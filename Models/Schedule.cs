using System;
using System.Text.Json.Serialization;
namespace CafesAPI.Models
{
    public class Schedule
    {
        [JsonIgnore]
        public int ScheduleID { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        //[JsonIgnore]
        public int CafeId { get; set; }

    }
}
