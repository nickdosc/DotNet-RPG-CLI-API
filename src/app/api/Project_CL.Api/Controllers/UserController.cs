namespace Project_CL.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Project_CL.Data.user;
    using Project_CL.Data.context;
    using Microsoft.EntityFrameworkCore;
    using System;


    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>();
        private readonly Project_CL_Context _context;
        public UserController(Project_CL_Context context)
        {
            _context = context;
        }

        // GET:
        [HttpGet(Name = "getusers")]
        public ActionResult<List<User>> GetUsers()
        {

            var users = _context.Users.ToList();
            
            return Ok(users);
        }

        //Get User for login by name
        [HttpGet("{username}", Name = "getuser")]
        public ActionResult<User> GetUser(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username== username);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: 
        [HttpPost(Name = "createuser")]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            if (users.Exists(u => u.Username == newUser.Username))
            {
                return BadRequest("User already exists.");
            }
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok(newUser);
        }
      
    }

}
