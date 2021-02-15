using System;

namespace DatingApp.API.Dtos
{
    public class PhotoForReturnDto
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Decription {set;get;}
        public DateTime DateAdd{set;get;}
        public bool IsMain {set;get;}
        public string PublicID { get; set; }
    }
}