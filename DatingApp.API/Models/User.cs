using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Models
{
    public class User
    {
        public int id { get; set; }
        [Required]
        public string UserName{set;get;}
        public byte[] PasswordHash{set;get;}
        public byte[] PasswordSalt{set;get;}
    }
}