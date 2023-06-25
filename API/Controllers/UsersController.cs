using System.Diagnostics;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // GET /api/users
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet] //Api endpoint so we can request a list of users from our DB

        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //task is an asyncronous action that can return a value
        {
            var users = await _context.Users.ToListAsync();  //a list of users from my DB

            return users;
        }

        //return an indivisual user
         [HttpGet("{id}")]
         public async Task<ActionResult<AppUser>> GetUser(int id)
         {
            return await _context.Users.FindAsync(id);
            //var user = _context.Users.Find(id); // find user with a specific id(primary key)
            //return user;
         }
           

    }

}
