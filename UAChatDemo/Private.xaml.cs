using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UAChatDemo.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public int Id { get; set; }
        public ObservableCollection<Messages> MessagesList { get; set; }

        public Private()
        {
            this.InitializeComponent();
            MessagesList = new ObservableCollection<Messages>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.Id = (int)e.Parameter;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog(this.Id.ToString());
            await dialog.ShowAsync();
        }
    }


}
