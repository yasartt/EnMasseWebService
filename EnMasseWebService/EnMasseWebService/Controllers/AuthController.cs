using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EnMasseWebService.Models;
using EnMasseWebService.Models.DTOs;
using EnMasseWebService.Services;

namespace EnMasseWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
             _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> Login(LoginDTO loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);

            if (loginResult.Item1) // If login is successful
            {
                return Ok(new
                {
                    Token = loginResult.Item2, // JWT token
                    User = loginResult.Item3 // UserDTO
                });
            }
            else
            {
                return BadRequest(new { ErrorMessage = loginResult.Item2 });
            }
        }

        /**[HttpPost]
        public async Task<ActionResult<string>> SignUp(SignUpDTO signUpDTO)
        {
            return BadRequest();
        }*/
    }
}
