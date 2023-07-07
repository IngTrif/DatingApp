using System.Diagnostics;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
   [Authorize]
    public class UsersController : BaseApiController //save of repeating yourself
    {
        //private readonly DataContext _context;

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, 
               IPhotoService photoService)
        {
            _photoService = photoService;
           _userRepository = userRepository;
           _mapper = mapper;
            
        }

        //[AllowAnonymous] //You cannot used at hightest level
        [HttpGet] //Api endpoint so we can request a list of users from our DB

        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() //task is an asyncronous action that can return a value
        {
          var users = await _userRepository.GetMembersAsync();  //a list of users from my DB  
          
          //var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
          return Ok(users); 
        }

        //return an indivisual user
         [HttpGet("{username}")]
         public async Task<ActionResult<MemberDto>> GetUser(string username)
         {
           return await _userRepository.GetMemberAsync(username);
           //return _mapper.Map<MemberDto>(user);
            //var user = _context.Users.Find(id); // find user with a specific id(primary key)
            //return user;
         }

            [HttpPut]
            public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
            {
               //var username = User.GetUsername();
               var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

               if (user == null) return NotFound();

               _mapper.Map(memberUpdateDto, user);

               if (await _userRepository.SaveAllAsync()) return NoContent();

               return BadRequest("Failed to update user"); 
            }

            [HttpPost("add-photo")]

            public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
            {
               var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

               if (user == null) return NotFound();

               var result = await _photoService.AddPhotoAsync(file);

               if (result.Error != null) return BadRequest(result.Error.Message);

               var photo = new Photo
               {
                  Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
               };

               if (user.Photos.Count == 0) photo.IsMain = true;

               user.Photos.Add(photo);

               if (await _userRepository.SaveAllAsync()) //return _mapper.Map<PhotoDto>(photo);
               {
                  return CreatedAtAction(nameof(GetUser), 
                     new {username = user.UserName}, _mapper.Map<PhotoDto>(photo));
               }

               return BadRequest("Problem adding photo");
            }
         }
           

    }


