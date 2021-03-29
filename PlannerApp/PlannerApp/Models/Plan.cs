using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.Models
{
    public class Plan
    {
        
        [PrimaryKey, AutoIncrement]
        public int PlanID { get; set; }
        public string PlanTitle { get; set; }
        public string PlanDescription{ get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public string Days => TimeSpan.Days.ToString("00");
        public string Hours => TimeSpan.Hours.ToString("00");
        public string Minutes => TimeSpan.Minutes.ToString("00");
        public string Seconds => TimeSpan.Seconds.ToString("00");
        public string BgColor { get; set; }

    }
}
