using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public IConfiguration _config { get; }

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

       [HttpGet("getusers")]
       public async Task<IActionResult> GetUsers()
       {
           var users = await _repo.GetUsers();
           return Ok(users);
       }


        [HttpPost("register")]

        public async Task<IActionResult> Register(UserForRegisterDTO model)
        {
            
            model.Username = model.Username.ToLower();

            if (!await _repo.UserExists(model.Username))
            {
                var userToCreate = new User
                {
                    Username = model.Username
                };

                var createdUser = _repo.Register(userToCreate, model.Password);

                return StatusCode(201); //Hack we need to user CreateActionAtRoute
            }

            return BadRequest("Username Already Exits");

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDto)
        {

            
            
            var userfromrepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userfromrepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier , userfromrepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userfromrepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }


    }
}