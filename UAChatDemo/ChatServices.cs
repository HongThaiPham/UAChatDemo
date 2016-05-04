using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UAChatDemo.Models;
using Windows.Storage;

namespace UAChatDemo
{
    class ChatServices
    {
        public static async Task GetHistory(ObservableCollection<History> history)
        {
            var setting = ApplicationData.Current.LocalSettings;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", setting.Values["AccessToken"].ToString());
            var respone = await client.GetAsync("https://meapi.lhu.edu.vn/api/chat/history/2016-04-19");
            var json = await respone.Content.ReadAsStringAsync();
            var returnData = JsonConvert.DeserializeObject<Histories>(json);
            var listHis = returnData.data.results;
            foreach (History item in listHis)
            {
              history.Add(item);
            }
        }
    }
}
