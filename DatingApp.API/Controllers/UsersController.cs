using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDatingRepository _datingRepository;
        private readonly IMapper _mapper;
        public UsersController(IDatingRepository datingRepository, IMapper mapper)
        {
            _mapper = mapper;
            _datingRepository = datingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _datingRepository.GetUsers();
            var usersforlist =_mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersforlist);
        }

        [HttpGet("{UserID}")]
        public async Task<IActionResult> GetUser(int UserID)
        {
            var user = await _datingRepository.GetUser(UserID);
            var userToReturn = _mapper.Map<UserForDetailDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{UserID}")]
        public async Task<IActionResult> UpdateUser(int userID, UserForUpdateDto userForUpdateDto)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            return Unauthorized();

            var userFromRepo = await _datingRepository.GetUser(userID);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _datingRepository.SaveAll())
            return NoContent();

            throw new Exception($"Updatinh user {userID} failed on save");
        }
    }
}