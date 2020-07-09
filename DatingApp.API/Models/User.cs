using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName{set;get;}
        public byte[] PasswordHash{set;get;}
        public byte[] PasswordSalt{set;get;}
        public string Gender {get;set;}
        public DateTime DateofBith {set;get;}
        public string KnowAs {set;get;}
        public DateTime CreatedDate{set;get;}
        public DateTime LastActive {set;get;}
        public string Intoduction {set;get;}
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City {set;get;}
        public string Country {set;get;}
        public ICollection<Photo> Photos {set;get;}
    }
}