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
        public async Task<ActionResult<Daily>> AddNewDaily(DailyDTO newDailyDTO)
        {
            if (newDailyDTO == null)
            {
                return BadRequest();
            }

            var newDaily = await _dailyService.AddNewDailyAsync(newDailyDTO);

            if (newDaily != null && newDailyDTO.Images != null && newDailyDTO.Images.Any())
            {
                var uploadedImages = await _dailyService.UploadImagesAsync(newDailyDTO.Images, newDaily.DailyId);
            }

            return Ok(newDaily);
        }

        [HttpGet("{dailyId}")]
        public async Task<ActionResult<List<ImageDTO>>> GetImagesByDailyId(int dailyId)
        {
            var images = await _dailyService.GetImagesByDailyIdAsync(dailyId);

            if (images == null || images.Count == 0)
            {
                return NotFound();
            }

            return images;
        }

        [HttpGet("{userId}/{lastTime}")]
        public async Task<ActionResult<List<DailyView>>> GetContactDailiesByUser(int userId, DateTime lastTime)
        {
            var dailies = await _dailyService.GetContactDailiesByUserIdAsync(userId, lastTime);

            if (dailies == null || dailies.Count == 0)
            {
                return NotFound();
            }

            return dailies;
        }
    }
}
