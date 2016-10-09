using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Reminder_beta
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPage : Page
    {
        public EditPage()
        {
            this.InitializeComponent();
          




    }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var indeks = e.Parameter as string;
            App.indeks1 = indeks;
            
            ReminderData Data = new ReminderData();
            Data = App.ListOfData[int.Parse(indeks)];
            WhatToRemind.Text = Data.Name;
            Quantity.Text = Data.Quantity.ToString();
            UnitBox.SelectedIndex = Data.Unit;      //     TODO: Unitbox selected item when on edit page!!
            TimePicker.Time = Data.Time;
            if (Data.afterMeal == true)
                AfterMeal.IsChecked = true;
            if (Data.beforeMeal == true)
                BeforeMeal.IsChecked = true;
            
            foreach(DayOfWeek element in Data.WeekDays)
            {
                switch (element)
                {
                    case DayOfWeek.Monday:
                        Monday.IsChecked = true;
                        break;
                    case DayOfWeek.Tuesday:
                        Tuesday.IsChecked = true;
                        break;
                    case DayOfWeek.Wednesday:
                        Wednesday.IsChecked = true;
                        break;
                    case DayOfWeek.Thursday:
                        Thursday.IsChecked = true;
                        break;
                    case DayOfWeek.Friday:
                        Friday.IsChecked = true;
                        break;
                    case DayOfWeek.Saturday:
                        Saturday.IsChecked = true;
                        break;
                    case DayOfWeek.Sunday:
                        Sunday.IsChecked = true;
                        break;
                        
                }
            }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            SetReminderData();
           
           
            bool check = await SetupPage.SaveMyData(App.ListOfData);
            rootFrame.BackStack.Remove(rootFrame.BackStack.Last());
            rootFrame.BackStack.Remove(rootFrame.BackStack.Last());




        }
        private async void SetReminderData()
        {
            int indeks = int.Parse(App.indeks1);
            List<DayOfWeek> weekdays = new List<DayOfWeek>();
            TimeSpan SelectedTime = new TimeSpan();
            weekdays = selectedDays();
            bool beforemeal;
            bool aftermeal;
            if (AfterMeal.IsChecked == true)
                aftermeal = true;
            else
                aftermeal = false;
            if (BeforeMeal.IsChecked == true)
                beforemeal = true;
            else
                beforemeal = false;
            SelectedTime = SelectedTime.Add(TimePicker.Time);

           // ReminderData Data = new ReminderData(WhatToRemind.Text, beforemeal, aftermeal, int.Parse(Quantity.Text), UnitBox.SelectedIndex, SelectedTime/*TimePicker.Time.Hours, TimePicker.Time.Minutes*/, weekdays);
          //  App.ListOfData[indeks] = Data;
         //   ScheduledNotification toast = new ScheduledNotification();
         //   toast.setEverything(Data);
            try
            {
                if (weekdays.Count() == 0)
                {
                    throw new CustomException();

                }
                else
                {
                    ReminderData Data = new ReminderData(WhatToRemind.Text, beforemeal, aftermeal, int.Parse(Quantity.Text), UnitBox.SelectedIndex, SelectedTime/*TimePicker.Time.Hours, TimePicker.Time.Minutes*/, weekdays);

                    App.ListOfData[indeks] = Data;
                    



                    ScheduledNotification toast = new ScheduledNotification();
                    toast.setEverything(Data);

                    this.Frame.Navigate(typeof(Reminders));
                }
            }
            catch (CustomException ex)
            {
                var dialog = new MessageDialog("Select Day", "Error!");
                await dialog.ShowAsync();
            }
            catch
            {
                var dialog = new MessageDialog("Enter Quantity", "Error!");
                await dialog.ShowAsync();

            }
        }
        private List<DayOfWeek> selectedDays()
        {
            List<DayOfWeek> days = new List<DayOfWeek>();
            if (Monday.IsChecked == true)
                days.Add(DayOfWeek.Monday);


            if (Tuesday.IsChecked == true)
                days.Add(DayOfWeek.Tuesday);

            if (Wednesday.IsChecked == true)
                days.Add(DayOfWeek.Wednesday);

            if (Thursday.IsChecked == true)
                days.Add(DayOfWeek.Thursday);

            if (Friday.IsChecked == true)
                days.Add(DayOfWeek.Friday);

            if (Saturday.IsChecked == true)
                days.Add(DayOfWeek.Saturday);

            if (Sunday.IsChecked == true)
                days.Add(DayOfWeek.Sunday);


            return days;
        }

    }
}
