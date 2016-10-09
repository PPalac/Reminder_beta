using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProject1
{
    public class DataToRefresh
    {
        public string Name { get; set; }
        public bool beforeMeal { get; set; }
        public bool afterMeal { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        // public int Hour;
        //public int Minutes;
        public List<DayOfWeek> WeekDays { get; set; }
        public TimeSpan Time { get; set; }


        public DataToRefresh(string Name, bool beforeMeal, bool afterMeal, int Quantity, int Unit, TimeSpan Time/*int Hour, int Minutes*/, List<DayOfWeek> WeekDays)
        {
            this.Name = Name;
            this.beforeMeal = beforeMeal;
            this.afterMeal = afterMeal;
            this.Quantity = Quantity;
            this.Unit = Unit;
            //  this.Hour = Hour;
            // this.Minutes = Minutes;
            this.Time = Time;
            this.WeekDays = WeekDays;


        }
        public DataToRefresh()
        {

        }
    }
}
