

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using System.IO;
using System.Runtime.Serialization;
using Windows.Storage;




namespace Background
{
    public sealed class BTask : IBackgroundTask
    {
        BackgroundTaskDeferral def;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {

            Data ToRefresh = new Data();
            List<string> name = new List<string>();
            List<bool> before = new List<bool>();
            List<bool> after = new List<bool>();
            List<int> quantity = new List<int>();
            List<int> indexUnit = new List<int>();
            List<TimeSpan> time = new List<TimeSpan>();
            List<List<DayOfWeek>> days = new List<List<DayOfWeek>>();
            List<Data> ToRefreshList = new List<Data>();
            def = taskInstance.GetDeferral();
            name = await GetMyDataName("name.txt");
            before = await GetMyDataWhen("before.txt");
            after = await GetMyDataWhen("after.txt");
            quantity = await GetMyDataInt("quantity.txt");
            indexUnit = await GetMyDataInt("indexUnit.txt");
            time = await GetMyDataTime("time.txt");
            days = await GetMyDataDays("days.txt");
             
           
            foreach(string element in name)
            {
                ToRefresh.Name = element;

                ToRefresh.beforeMeal = before.First();
                before.Remove(before.First());

                ToRefresh.afterMeal = after.First();
                after.Remove(after.First());

                ToRefresh.Quantity = quantity.First();
                quantity.Remove(quantity.First());

                ToRefresh.Unit = indexUnit.First();
                indexUnit.Remove(indexUnit.First());

                ToRefresh.Time = time.First();
                time.Remove(time.First());

                ToRefresh.WeekDays = days.First();
                days.Remove(days.First());

                ToRefreshList.Add(ToRefresh);

            }
            Refresh toast = new Refresh();
            foreach(Data element in ToRefreshList)
            {
                toast.setEverything(element);
            }
            def.Complete();



        }
     
        
        
        
        
        
        
        
      
        
        #region Saving / Loading MyData 
        

         static async Task<List<string>> GetMyDataName(string _myFileLocation)
        {
            // If you're saving your stuff just on this device
            var readStream =
                await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

            // If there is no MyData, then we haven't created our MyData file yet
            if (readStream == null)
                return new List<string>();

            
            DataContractSerializer stuffSerializer =
                new DataContractSerializer(typeof(List<string>));
           

            var setResult = (List<string>)stuffSerializer.ReadObject(readStream);
            
            
            return setResult;
        }

        static async Task<List<bool>> GetMyDataWhen(string _myFileLocation)
        {
            // If you're saving your stuff just on this device
            var readStream =
                await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

            // If there is no MyData, then we haven't created our MyData file yet
            if (readStream == null)
                return new List<bool>();


            DataContractSerializer stuffSerializer =
                new DataContractSerializer(typeof(List<bool>));


            var setResult = (List<bool>)stuffSerializer.ReadObject(readStream);


            return setResult;
        }

        static async Task<List<int>> GetMyDataInt(string _myFileLocation)
        {
            // If you're saving your stuff just on this device
            var readStream =
                await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

            // If there is no MyData, then we haven't created our MyData file yet
            if (readStream == null)
                return new List<int>();


            DataContractSerializer stuffSerializer =
                new DataContractSerializer(typeof(List<int>));


            var setResult = (List<int>)stuffSerializer.ReadObject(readStream);


            return setResult;
        }

        static async Task<List<List<DayOfWeek>>> GetMyDataDays(string _myFileLocation)
        {
            // If you're saving your stuff just on this device
            var readStream =
                await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

            // If there is no MyData, then we haven't created our MyData file yet
            if (readStream == null)
                return new List<List<DayOfWeek>>();


            DataContractSerializer stuffSerializer =
                new DataContractSerializer(typeof(List<List<DayOfWeek>>));


            var setResult = (List<List<DayOfWeek>>)stuffSerializer.ReadObject(readStream);


            return setResult;
        }

        static async Task<List<TimeSpan>> GetMyDataTime(string _myFileLocation)
        {
            // If you're saving your stuff just on this device
            var readStream =
                await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(_myFileLocation);

            // If there is no MyData, then we haven't created our MyData file yet
            if (readStream == null)
                return new List<TimeSpan>();


            DataContractSerializer stuffSerializer =
                new DataContractSerializer(typeof(List<TimeSpan>));


            var setResult = (List<TimeSpan>)stuffSerializer.ReadObject(readStream);


            return setResult;
        }

        #endregion
    }
}
