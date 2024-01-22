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
    public class DailyController : Controller
    {
        private readonly DailyService _dailyService;
        public DailyController(DailyService dailyService)
        {

            _dailyService = dailyService;

        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddNewDaily(DailyDTO newDailyDTO)
        {
            if (newDailyDTO == null)
            {
                return BadRequest();
            }

            
            
            return Ok(newDailyDTO);
            
        }
    }
}
