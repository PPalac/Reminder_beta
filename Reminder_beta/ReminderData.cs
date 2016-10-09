using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;




namespace Reminder_beta
{
    
    public class ReminderData
    {
        public string Name;
        public bool beforeMeal;
        public bool afterMeal;
        public int Quantity;
        public int Unit;
       // public int Hour;
        //public int Minutes;
        public List<DayOfWeek> WeekDays = new List<DayOfWeek>();
       public TimeSpan Time = new TimeSpan();

        public ReminderData(string Name, bool beforeMeal, bool afterMeal, int Quantity, int Unit, TimeSpan Time/*int Hour, int Minutes*/, List<DayOfWeek> WeekDays)
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
        public ReminderData()
        {

        }
    }
}
