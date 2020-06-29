using System.ComponentModel.DataAnnotations;
namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage="you must enter user name")] 
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}