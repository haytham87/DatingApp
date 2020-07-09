using System;
using System.Collections.Generic;
using DatingApp.API.Models;

namespace DatingApp.API.Dtos
{
    public class UserForDetailDto
    {
        public int UserID { get; set; }
        public string UserName{set;get;}
        public string Gender {get;set;}
        public int Age {set;get;}
        public string KnowAs {set;get;}
        public DateTime CreatedDate{set;get;}
        public DateTime LastActive {set;get;}
        public string Intoduction {set;get;}
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City {set;get;}
        public string Country {set;get;}
        public string PhotoUrl { get; set; }
        public ICollection<PhotoForDetailedDto> photos { set; get;}
    }
}