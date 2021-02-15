using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users/{userID}/photos")]
    public class PhotoController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public PhotoController(IDatingRepository repo, IMapper mapper, 
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{iD}", Name ="GetPhoto")]
        public async Task<IActionResult> GetPhoto(int iD)
        {
            var photoFromRepo = await _repo.GetPhoto(iD);
            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);
            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userID,
             [FromForm]PhotoForCreateDto photoForCreateDto){

             if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userID);
            var file = photoForCreateDto.File;

            var uploadResult= new ImageUploadResult();

            if(file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name , stream),
                        Transformation = new Transformation().Width(500).Height(500).
                        Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }

            }
            photoForCreateDto.Url = uploadResult.Url.ToString();
            photoForCreateDto.PublicID = uploadResult.PublicId;
            
            var photo = _mapper.Map<Photo>(photoForCreateDto);

            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

                userFromRepo.Photos.Add(photo);
                if (await _repo.SaveAll())
                {
                    var photoForReturn =_mapper.Map<PhotoForReturnDto>(photo);
                    return CreatedAtRoute("GetPhoto", new { userID = userID , iD =photo.ID}, photoForReturn);
                }

                return BadRequest("Cloud not add the photot");
        }

        [HttpPost("{iD}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userID,int iD)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repo.GetUser(userID);
            if (!user.Photos.Any( p => p.ID == iD))
            return Unauthorized();

            var photoFromRepo = await _repo.GetPhoto(iD);
            if (photoFromRepo.IsMain)
             return BadRequest("this is aleady main photo");

             var currentMainPhoto = await _repo.GetMainPhotoForUser(userID);
             currentMainPhoto.IsMain= false;

             photoFromRepo.IsMain = true;
             if (await _repo.SaveAll())
             return NoContent();

             return BadRequest("Could not Set photo to Main");
        }
    }
}