using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManage.Models;

namespace UserManage.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>();

        // GET: api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET: api/users/5
        [HttpGet("{userid}", Name = "Get")]
        public User Get(Guid userid)
        {
            var user = users.Where(x => x.UserId == userid).FirstOrDefault();
            return user;
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            if (value == null)
            {
                return new BadRequestResult();
            }

            if (value.UserEmail == null)
            {
                return BadRequest("Failure, Email Missing which is required.");
            }

            if (value.UserPassword == null)
            {
                return BadRequest("Failure, Password Missing which is required.");
            }
            value.UserId = Guid.NewGuid();
            value.DateAdded = DateTime.UtcNow;
            value.UserPassword = Encrypt.EncryptNow(value.UserPassword);
            users.Add(value);


            var result = new { UserId = value.UserId, Password_Encrypted = value.UserPassword, Success = true };
            return CreatedAtAction(nameof(Get), new { userid = value.UserId }, result);
        }

        // PUT: api/users/<guid>
        [HttpPut("{userid}")]
        public IActionResult Put(Guid userid, [FromBody] User value)
        {

            var user = users.Where(x => x.UserId == userid).FirstOrDefault();

            if (userid == null)
            {
                return BadRequest("UserID Missing");
            }

            if (user == null)
            {
                return BadRequest("UserID Does not exist");
            }

            if (value.UserPassword == null)
            {

            }
            else
            {
                user.UserPassword = Encrypt.EncryptNow(value.UserPassword);
            }

            if (value.UserEmail == null)
            {

            }
            else
            {
                user.UserEmail = value.UserEmail;
            }



            var result = new { UserId = user.UserId, Name = user.UserEmail, Success = true };
            return Ok(user);
        }

        // DELETE: api/users/<guid>
        [HttpDelete("{userid}")]
        public IActionResult Delete(Guid userid)
        {
            if(userid == null)
            {
                return BadRequest("UserID Missing");
            }

            if (users.Where(x => x.UserId == userid).FirstOrDefault() == null)
            {
                return BadRequest("UserID Does not exist");
            }

            var user = users.Where(x => x.UserId == userid).FirstOrDefault();
            users.Remove(user);

            var result = new { UserId = user.UserId, Name = user.UserEmail, Deleted = true };
            return Ok(user);
        }
    }
}