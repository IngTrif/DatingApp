

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Controllers
{
   // [AllowAnonymous]
       // [Route("api/[controller]")]
      //  [ApiController]
    
    public class AccountController : BaseApiController
    {
        private ITokenService _tokenService;
        private readonly DataContext _context;
        public AccountController(DataContext context, ITokenService tokenService) //using th einterface here
        {
            _tokenService = tokenService;
            _context = context;
            
        }
        
        [HttpPost("register")] //Post: api/account/register
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto) //automatic binds
        {
            if (await UserExist(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512(); // randomly generate key used as a password salt

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(), //get the username from register
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto

            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        //prevent user if already exist

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("invalid username");  

            using var hmac = new HMACSHA512 (user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i=0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
            } 

             return new UserDto

            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };        
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}