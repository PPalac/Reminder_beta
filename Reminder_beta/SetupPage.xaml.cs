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
using Windows.Storage;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Windows.UI.Notifications;
using Windows.UI.Core;
using Windows.UI.Popups;
using Background;





// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Reminder_beta
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SetupPage : Page 
    {

        public SetupPage()
        {
            this.InitializeComponent();
          
            

        }
            
    

    

        private async void button_Click(object sender, RoutedEventArgs e)

        {
            Frame rootFrame = Window.Current.Content as Frame;
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

            try
            {
                if (weekdays.Count() == 0)
                {
                    throw new CustomException();

                }
                else
                {
                     ReminderData Data = new ReminderData(WhatToRemind.Text, beforemeal, aftermeal, int.Parse(Quantity.Text), UnitBox.SelectedIndex, SelectedTime, weekdays);
                    
                    App.ListOfData.Add(Data);
                    bool check = await SaveMyData(App.ListOfData);
                    


                    



                    ScheduledNotification toast = new ScheduledNotification();
                    toast.setEverything(Data);

                    this.Frame.Navigate(typeof(Reminders));
                    rootFrame.BackStack.Remove(rootFrame.BackStack.Last());
                    rootFrame.BackStack.Remove(rootFrame.BackStack.Last());
                }
            }
            catch (CustomException ex)
            {
                var dialog = new MessageDialog("Select Day", "Error!");
               await  dialog.ShowAsync();
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
        private bool isDaySelected(List<DayOfWeek> days)
        {
            if (days.Count == 0)
                return false;
            else
                return true;
        }


        #region Saving / Loading MyData 
        private const string _myFileLocation = "SavedData.txt";
       
        

        public static async Task<List<ReminderData>> GetMyData()
        {
            // If you're saving your stuff just on this device
            var readStream =
                await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

            // If there is no MyData, then we haven't created our MyData file yet
            if (readStream == null)
                return new List<ReminderData>(); ;

            DataContractSerializer stuffSerializer =
                new DataContractSerializer(typeof(List<ReminderData>));

            var setResult = (List<ReminderData>)stuffSerializer.ReadObject(readStream);

            return setResult;
        }

        public static async Task<bool> SaveMyData(List<ReminderData> saveData)
        {
            try
            {

                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocation,
                    CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(List<ReminderData>));

                    stuffSerializer.WriteObject(writeStream, saveData);
                    await writeStream.FlushAsync();
                    writeStream.Dispose();
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception("ERROR: unable to save MyData", e);
                //return false;
            }
        }

     
        #endregion



    }
}
