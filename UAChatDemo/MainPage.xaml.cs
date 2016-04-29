using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private bool incall = false, endoflist = false;
        private int offset = 0;
        public static ObservableCollection<string> History = new ObservableCollection<string>();

        public MainPage()
        {
            this.InitializeComponent();
            lvHistory.ItemsSource = History;
            fetchHstory(0);
            

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
                var dialog = new MessageDialog(setting.Values["AccessToken"].ToString());
                await dialog.ShowAsync();
            }
        }

        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer) return depObj as ScrollViewer;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);
                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }

        private void lvHistory_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer viewer = GetScrollViewer(this.lvHistory);
            viewer.ViewChanged += Viewer_ViewChanged;
        }

        private void Viewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer view = (ScrollViewer)sender;
            double progresss = view.VerticalOffset / view.ScrollableHeight;
            if (progresss > 0.7 && !incall && !endoflist)
            {
                incall = true;
                fetchHstory(++offset);
            }
        }

        private void fetchHstory(int v)
        {
            int start = v * 20;
            for (int i = start; i < start + 20; i++)
            {

            }
        }
    }
}
