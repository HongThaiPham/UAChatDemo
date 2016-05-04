using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAChatDemo.Models
{
    public class History
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Lasttime { get; set; }
        /// <summary>
        /// [0:text,1:emoticon,2:file,3:image]
        /// </summary>

        public int Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Readed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsMe { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOnline { get; set; }
        public string Avatar { get; set; }
    }

    public class DataHis
    {
        public int offet { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public int limit { get; set; }
        public List<History> results { get; set; }
    }
    public class Histories
    {
        public DataHis data { get; set; }
    }
}
