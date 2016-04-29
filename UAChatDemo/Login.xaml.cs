using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            myProgress.IsActive = !myProgress.IsActive;
            btnLogin.IsEnabled = !btnLogin.IsEnabled;

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://auth.lhu.edu.vn/");
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            HttpContent requestContent = new StringContent("grant_type=password&client_id=dcff83ded88e4f26af0d88c483329e2f&username=" + txtUsername.Text + "&password=" + txtPassword.Password, Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpResponseMessage responseMessage = await client.PostAsync("lhu/token", requestContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                string jsonMessage;
                using (Stream responseStream = await responseMessage.Content.ReadAsStreamAsync())
                {
                    jsonMessage = new StreamReader(responseStream).ReadToEnd();
                }

                TokenModel tokenResponse = (TokenModel)JsonConvert.DeserializeObject(jsonMessage, typeof(TokenModel));
                var setting = ApplicationData.Current.LocalSettings;
                setting.Values["AccessToken"] = tokenResponse.AccessToken;
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                var dialog = new MessageDialog(responseMessage.ReasonPhrase);
                await dialog.ShowAsync();
            }
            myProgress.IsActive = false;
            btnLogin.IsEnabled = true;
        }
    }
}
