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
        public static async Task GetHistory(ObservableCollection<History> history, string time)
        {
            var setting = ApplicationData.Current.LocalSettings;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", setting.Values["AccessToken"].ToString());
            var respone = await client.GetAsync(string.Format("https://meapi.lhu.edu.vn/api/chat/history/{0}", time));
            var json = await respone.Content.ReadAsStringAsync();
            var returnData = JsonConvert.DeserializeObject<Histories>(json);
            var listHis = returnData.data.results;
            foreach (History item in listHis)
            {
                item.Avatar = string.Format("https://file.lhu.edu.vn/avatar/{0}.jpg", item.UserId);
                history.Add(item);
            }
        }

      public static async Task GetChatMessage(ObservableCollection<string> messages, string time)
      {
      }
    }
}
