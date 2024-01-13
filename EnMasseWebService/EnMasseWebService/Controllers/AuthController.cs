using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EnMasseWebService.Models;
using EnMasseWebService.Models.DTOs;
using EnMasseWebService.Services;

namespace EnMasseWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
             _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginDTO loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);

            if(loginResult == 0)
            {
                return Ok("The token");
            }
            else if(loginResult == -1)
            {
                return BadRequest("What the fuck is this");
            }
            else if (loginResult == -2)
            {
                return NotFound("User not found");
            }
            else if(loginResult == -3)
            {
                return BadRequest("Wrong password!");
            }

            return BadRequest("");

        }

        /**[HttpPost]
        public async Task<ActionResult<string>> SignUp(SignUpDTO signUpDTO)
        {
            return BadRequest();
        }*/
    }
}
