using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;


namespace Background
{
    public sealed class Refresh
    {
        private DateTimeOffset DateAndTime = new DateTimeOffset();
        private Data Data;





        public void CreateScheduledNotification()
        {

            
            string name = Data.Name;
            var notifier = ToastNotificationManager.CreateToastNotifier();
            foreach (var element in notifier.GetScheduledToastNotifications())
            {
                if (element.Tag == name && element.DeliveryTime.Hour == DateAndTime.Hour && element.DeliveryTime.Minute == DateAndTime.Minute)
                    return;
                    
            }
            int quantity = Data.Quantity;
            string unit, beforeorafter;
            if (Data.Unit == 0)
            {
                unit = "ml";
            }
            else
            {
                unit = "pcs";
            }

            if (Data.beforeMeal == true && Data.afterMeal == false)
            {
                beforeorafter = "Before meal!";
            }
            else
            {
                if (Data.beforeMeal == true && Data.afterMeal == true)
                {
                    beforeorafter = "You can take it before or after meal";
                }
                else
                {
                    if (Data.beforeMeal == false && Data.afterMeal == true)
                    {
                        beforeorafter = "After meal!";
                    }
                    else
                    {
                        beforeorafter = "-----------------";
                    }
                }
            }




            string xml = $@"<toast scenario=""reminder"">
            <visual>
            <binding template=""ToastGeneric"">
                <text>{name}</text>
                <text>{quantity} {unit}</text>
                <text>{beforeorafter}</text>
                
            </binding>
            </visual>
            <actions>
            <input id=""snoozeTime"" type=""selection"" defaultInput=""10"">
                <selection id=""5"" content=""5 minutes""/>
                <selection id=""10"" content=""10 minutes""/>
                <selection id=""15"" content=""15 minutes""/>
            </input>
            <action activationType=""system"" arguments=""snooze"" hint-inputId=""snoozeTime"" content=""""/>
            <action activationType=""system"" arguments=""dismiss"" content=""""/>
            </actions>
            <audio src = ""ms-appx:///Assets/toast.mp3"" loop = ""true""></audio>
            </toast>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
         //   DateTimeOffset test = DateTimeOffset.Now.AddMinutes(1); //do usuniecia, tylko do testo!!!
            ScheduledToastNotification toast = new ScheduledToastNotification(doc,DateAndTime);
            toast.Tag = name;
            
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);



        }
        internal void setEverything(Data DataFromList)
        {
            DateTime now = DateTime.Now;
            DayOfWeek day = now.DayOfWeek;
            DateAndTime = DateTimeOffset.Now;
            Data = DataFromList;
            foreach (DayOfWeek element in DataFromList.WeekDays)
            {
                int i = day.CompareTo(element);
                if (i == -1)
                {
                    DateAndTime = DateAndTime.AddDays(element - day);
                }
                else
                {
                    if (i == 1)
                    {
                        DateAndTime = DateAndTime.AddDays(7 - (day - element));
                    }
                    else
                    {
                        if (DataFromList.Time.Hours <= now.Hour && DataFromList.Time.Minutes <= now.Minute)
                        {
                            DateAndTime = DateAndTime.AddDays(7);
                        }
                    }
                }
                if (DataFromList.Time.Hours.CompareTo(now.Hour) == -1)
                {
                    DateAndTime = DateAndTime.AddHours(DataFromList.Time.Hours - now.Hour);
                }
                else
                {
                    if (DataFromList.Time.Hours.CompareTo(now.Hour) == 1)
                    {
                        DateAndTime = DateAndTime.AddHours(DataFromList.Time.Hours - now.Hour);
                    }
                    //  else
                    //   {
                    //     DateAndTime = DateAndTime.AddHours(DataFromList.Time.Hours);
                    //  }
                }

                if (DataFromList.Time.Minutes.CompareTo(now.Minute) == -1)
                {
                    DateAndTime = DateAndTime.AddMinutes(DataFromList.Time.Minutes - now.Minute);
                }
                else
                {
                    if (DataFromList.Time.Minutes.CompareTo(now.Minute) == 1)
                    {
                        DateAndTime = DateAndTime.AddMinutes(DataFromList.Time.Minutes - now.Minute);
                    }
                    else
                    {
                        DateAndTime = DateAndTime.AddMinutes(DataFromList.Time.Minutes);
                    }
                }
                CreateScheduledNotification();

            }






        }

    }
}
