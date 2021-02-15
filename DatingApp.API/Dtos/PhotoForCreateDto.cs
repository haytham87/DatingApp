using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Dtos
{
    public class PhotoForCreateDto
    {
        public  string Url { get; set; }
        public IFormFile File {set;get;}
        public  string Description { get; set; }
        public DateTime DateAdd {set;get;}
        public string PublicID{set;get;}

        public PhotoForCreateDto()
        {
            DateAdd = DateTime.Now;
        }
    }
}