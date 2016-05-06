using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UAChatDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Private : Page
    {
        public History item { get; set; }
        private const string _URL = "https://lhu.edu.vn:5000";
        public ObservableCollection<Messages> MessagesList { get; set; }
        private Socket socket;

        public Private()
        {
            this.InitializeComponent();
            MessagesList = new ObservableCollection<Messages>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.item = (History)e.Parameter;
            var setting = ApplicationData.Current.LocalSettings;

            var opts = new IO.Options();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("token", setting.Values["AccessToken"].ToString());
            opts.Query = dictionary;
            socket = IO.Socket(_URL, opts);
            socket.Connect();

            JObject obj = new JObject();
            obj["userId"] = item.UserId;
            obj["userName"] = item.UserName;


            socket.On(Socket.EVENT_CONNECT, () =>
            {
                socket.Emit("joinroom", obj);
            });

            socket.On("joinroom", (data) =>
            {
                var s = (JObject)data;
                
            });
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog(JsonConvert.SerializeObject(item));
            await dialog.ShowAsync();
        }
    }


}
