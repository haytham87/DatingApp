using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.username =userForRegisterDto.username.ToLower();

            if(await _repo.CheckUserName(userForRegisterDto.username))
            return BadRequest("user name is already exist");

            var userToCreate=new User
            {
                UserName=userForRegisterDto.username
            };

            var createduser=_repo.Register(userToCreate,userForRegisterDto.password);

            return StatusCode(201);
        }

    }
}