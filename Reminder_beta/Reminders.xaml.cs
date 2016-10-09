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
using Windows.UI.Notifications;
using Windows.UI.Core;
using Windows.Storage;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Reminder_beta
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Reminders : Page
    {
        public Reminders()
        {
            this.InitializeComponent();

            createButton();
            bgRefresh();
           


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SetupPage));
        }




        private void createButton()
        {
            foreach (ReminderData element in App.ListOfData)
            {

                Button button = new Button();
                SymbolIcon deleteButton = new SymbolIcon();

                button.Content = element.Name;
                button.HorizontalContentAlignment = HorizontalAlignment.Center;
                button.VerticalContentAlignment = VerticalAlignment.Center;
                button.Width = 175;
                button.Height = 50;
                Thickness margin = button.Margin;
                margin.Top = 20;
                button.Margin = margin;
                button.Background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 246, 162, 0));
                button.FontSize = 20;
                button.IsHoldingEnabled = false;
                button.Name = App.ListOfData.IndexOf(element).ToString();
                
                
                button.Click += (s, e) =>
                {
                    var notifier = ToastNotificationManager.CreateToastNotifier();
                    var scheduled = notifier.GetScheduledToastNotifications();


                    for (int i = 0, len = scheduled.Count; i < len; i++)

                    {

                        if (scheduled[i].Tag == App.ListOfData[int.Parse(button.Name)].Name && scheduled[i].DeliveryTime.Hour == App.ListOfData[int.Parse(button.Name)].Time.Hours && scheduled[i].DeliveryTime.Minute == App.ListOfData[int.Parse(button.Name)].Time.Minutes)
                        {
                            notifier.RemoveFromSchedule(scheduled[i]);

                        }
                    }
                    this.Frame.Navigate(typeof(EditPage),button.Name);
                };


                deleteButton.Symbol = Symbol.Delete;
                deleteButton.Width = 50;
                deleteButton.Height = 50;
                deleteButton.Margin = margin;
                deleteButton.IsHoldingEnabled = false;
                deleteButton.Name = App.ListOfData.IndexOf(element).ToString();

                deleteButton.Tapped += async (s, e) =>
                {
                    var notifier = ToastNotificationManager.CreateToastNotifier();
                    var scheduled = notifier.GetScheduledToastNotifications();
                   
                   
                         for (int i = 0, len = scheduled.Count; i < len; i++)

                        {

                        if (scheduled[i].Tag == App.ListOfData[int.Parse(button.Name)].Name && scheduled[i].DeliveryTime.Hour == App.ListOfData[int.Parse(button.Name)].Time.Hours && scheduled[i].DeliveryTime.Minute == App.ListOfData[int.Parse(button.Name)].Time.Minutes)
                        {
                            notifier.RemoveFromSchedule(scheduled[i]);

                        }
                       }
                    App.ListOfData.RemoveAt(int.Parse(deleteButton.Name));
                    await  SetupPage.SaveMyData(App.ListOfData);
                    this.Frame.Navigate(typeof(Reminders));
                };


                listBox.Items.Add(button);
                listBox1.Items.Add(deleteButton);
                
            }

        }

        private async void  bgRefresh()
        {
            List<string> name = new List<string>();
            List<bool> before = new List<bool>();
            List<bool> after = new List<bool>();
            List<int> quantity = new List<int>();
            List<int> indexUnit = new List<int>();
            List<TimeSpan> time = new List<TimeSpan>();
            List<List<DayOfWeek>> days = new List<List<DayOfWeek>>();
            foreach (ReminderData element in App.ListOfData)
            {
                name.Add(element.Name);
                before.Add(element.beforeMeal);
                after.Add(element.afterMeal);
                quantity.Add(element.Quantity);
                indexUnit.Add(element.Unit);
                time.Add(element.Time);
                days.Add(element.WeekDays);

            }
            bool check = await SaveMyDataToRefresh(name, "name.txt");
            check = await SaveMyDataToRefresh(before, "before.txt");
            check = await SaveMyDataToRefresh(after, "after.txt");
            check = await SaveMyDataToRefresh(quantity, "quantity.txt");
            check = await SaveMyDataToRefresh(indexUnit, "indexUnit.txt");
            check = await SaveMyDataToRefresh(time, "time.txt");
            check = await SaveMyDataToRefresh(days, "days.txt");
        }

        public static async Task<bool> SaveMyDataToRefresh(List<int> saveData, string _myFileLocationToRefresh)
        {
            try
            {

                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocationToRefresh,
                    CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(List<int>));

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

        public static async Task<bool> SaveMyDataToRefresh(List<string> saveData, string _myFileLocationToRefresh)
        {
            try
            {

                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocationToRefresh,
                    CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(List<string>));

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
        public static async Task<bool> SaveMyDataToRefresh(List<bool> saveData, string _myFileLocationToRefresh)
        {
            try
            {

                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocationToRefresh,
                    CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(List<bool>));

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
        public static async Task<bool> SaveMyDataToRefresh(List<List<DayOfWeek>> saveData, string _myFileLocationToRefresh)
        {
            try
            {

                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocationToRefresh,
                    CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(List<List<DayOfWeek>>));

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
        public static async Task<bool> SaveMyDataToRefresh(List<TimeSpan> saveData, string _myFileLocationToRefresh)
        {
            try
            {

                StorageFile savedStuffFile =
                    await ApplicationData.Current.LocalFolder.CreateFileAsync(_myFileLocationToRefresh,
                    CreationCollisionOption.ReplaceExisting);

                using (Stream writeStream =
                    await savedStuffFile.OpenStreamForWriteAsync())
                {
                    DataContractSerializer stuffSerializer =
                        new DataContractSerializer(typeof(List<TimeSpan>));

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


    }
}
