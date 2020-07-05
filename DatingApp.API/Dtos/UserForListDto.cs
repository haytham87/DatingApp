using System;

namespace DatingApp.API.Dtos
{
    public class UserForListDto
    {
        public int id { get; set; }
        public string UserName{set;get;}
        public string Gender {get;set;}
        public int Age {set;get;}
        public string KnowAs {set;get;}
        public DateTime CreatedDate{set;get;}
        public DateTime LastActive {set;get;}
        public string Intoduction {set;get;}
        public string City {set;get;}
        public string Country {set;get;}
        public string PhotoUrl { get; set; }
    
    }
}