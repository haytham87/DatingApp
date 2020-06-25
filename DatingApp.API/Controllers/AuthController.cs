using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.username = userForRegisterDto.username.ToLower();

            if (await _repo.CheckUserName(userForRegisterDto.username))
                return BadRequest("user name is already exist");

            var userToCreate = new User
            {
                UserName = userForRegisterDto.username
            };

            var createduser = _repo.Register(userToCreate, userForRegisterDto.password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userrepo = await _repo.Login(userForLoginDto.username.ToLower(), userForLoginDto.password);

            if (userrepo == null)
                return Unauthorized();

            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier,userrepo.id.ToString()),
                 new Claim(ClaimTypes.Name,userrepo.UserName)
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor= new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };

            var tokenHandler=new JwtSecurityTokenHandler();

            var token=tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new{
              token=tokenHandler.WriteToken(token)
            });

        }

    }
}