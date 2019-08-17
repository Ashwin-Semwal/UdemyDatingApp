using System;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
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

    }
}