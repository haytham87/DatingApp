using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Decription {set;get;}
        public DateTime DateAdd{set;get;}
        public bool IsMain {set;get;}
        public  User user { get; set; }
        public int UserID{set;get;}
    }
}