using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EnMasseWebService.Models;
using EnMasseWebService.Models.DTOs;
using EnMasseWebService.Services;
using EnMasseWebService.Models.Entities;

namespace EnMasseWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /**[HttpPost]
        public async Task<ActionResult<bool>> SendContactRequest(ContactRequestDTO contactRequestDTO)
        {

        }*/
    }
}
