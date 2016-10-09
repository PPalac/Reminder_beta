using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background
{
     sealed  class Data
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
        

        public Data(string Name, bool beforeMeal, bool afterMeal, int Quantity, int Unit, TimeSpan Time/*int Hour, int Minutes*/, List<DayOfWeek> WeekDays)
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
        public Data()
        {

        }
    }
}
