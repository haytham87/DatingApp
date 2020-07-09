using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models
{
    public class Photo
    {
        [Key]
        public int ID { get; set; }
        public string Url { get; set; }
        public string Decription {set;get;}
        public DateTime DateAdd{set;get;}
        public bool IsMain {set;get;}
        public  User User { get; set; }
        public int UserID { get; set; }
    }
}