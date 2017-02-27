﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Background;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reminder_beta
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public  MainPage()
        {
            this.InitializeComponent();
            getData();
                                  

        }

        private void NavigateToReminders_click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Reminders));
        }

        private void NavigateToSettings_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Settings
        }
        private async Task getData()
        {
            App.ListOfData = await SetupPage.GetMyData();

        }
    }
}
