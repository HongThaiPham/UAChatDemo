using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UAChatDemo.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UAChatDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public ObservableCollection<History> HistoryList { get; set; }
        private const string _URL = "https://lhu.edu.vn:5000";

        public MainPage()
        {
            this.InitializeComponent();
            HistoryList = new ObservableCollection<History>();
          
        }




        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var setting = ApplicationData.Current.LocalSettings;
            if (setting.Values["AccessToken"] == null)
            {
                Frame.Navigate(typeof(Login));
            }
            else
            {
                //var dialog = new MessageDialog(setting.Values["AccessToken"].ToString());
                //await dialog.ShowAsync();

                myPrgress.IsActive = true;
                myPrgress.Visibility = Visibility.Visible;

                IO.Options opts = new IO.Options();
                //opts.Query.Add("token", setting.Values["AccessToken"].ToString());
                var socket = IO.Socket(_URL, opts);
                socket.Connect();

                await ChatServices.GetHistory(HistoryList, "9999-01-01");

                myPrgress.IsActive = false;
                myPrgress.Visibility = Visibility.Collapsed;

            }
        }

        private void lvHistory_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = (History)e.ClickedItem;
            Frame.Navigate(typeof(Private), 32);
        }
    }
}
