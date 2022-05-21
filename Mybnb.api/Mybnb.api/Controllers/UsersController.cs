#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mybnb.api.Data;
using Mybnb.api.Helpers;
using Mybnb.api.Models;
using Mybnb.dtolibrary.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Mybnb.dtolibrary.DTOs.User;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Mybnb.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MybnbapiContext _context;
        private readonly AppSettings _appSettings;

        public UsersController(MybnbapiContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUser createUser)
        {

            string hashedPassword = EncryptPassword(createUser.Password);

            User user = new User { Email = createUser.Email, FullName = createUser.FullName, Password = hashedPassword };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        //CREATE LOGIN HERE
        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> Authenticate(AuthenticateRequest model)
        {

            string hashedPassword = EncryptPassword(model.Password);

            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == hashedPassword);

            // return null if user not found
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            // authentication successful so generate jwt token
            var token = GenerateJWTToken(user);

            var response = new AuthenticateResponse(user.UserID, user.Email, token);

            return Ok(response);
        }
        [HttpGet]
        [Authorize]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            //Get Token and logout user 
            throw new NotImplementedException();
        }

        private string GenerateJWTToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string EncryptPassword(string password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            var algorithm = SHA256.Create();

            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedBytes.Length; i++)
            {
                builder.Append(hashedBytes[i].ToString("X2"));
            }
            string result = builder.ToString();
            
            return result;
        }
    }
}
