using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationHub.API.Data;
using NotificationHub.API.Services;
using NotificationHub.SharedLib.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationHub.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly INotificationHubService _notificationHubService;

        public ManagerController(ApplicationDbContext applicationDbContext, INotificationHubService notificationHubService)
        {
            _applicationDbContext = applicationDbContext;
            _notificationHubService = notificationHubService;
        }

        // Action methods 
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            List<ResponseUserDto> responseUserDtos = new List<ResponseUserDto>();
            await _applicationDbContext.Users.ForEachAsync(async user =>
            {
                ResponseUserDto responseUserDto = new ResponseUserDto(user.Username);

                responseUserDto.Devices = await _applicationDbContext.Devices
                    .Where(d => d.Username == user.Username)
                    .Select(d => new ResponseDeviceDto(d.Id, d.Platform, d.RegistrationId, d.PnsToken))
                    .ToListAsync();

                responseUserDtos.Add(responseUserDto);
            });

            return Ok(responseUserDtos);
        }

        [HttpPost("SendNotification")]
        public async Task<IActionResult> SendNotification(SendNotificationDto sendNotificationDto)
        {
            // Check if this user exists.
            Models.User user = await _applicationDbContext.Users
                .Where(u => u.Username == sendNotificationDto.Username)
                .FirstOrDefaultAsync();

            // If the user does not exist, return BadRequest
            if (user is null)
            {
                return BadRequest($"The user '{sendNotificationDto.Username}' does not exist.");
            }

            await _notificationHubService.SendNotification(sendNotificationDto.Message, $"User_{user.Username}");

            // If the user has no more devices left, delete them.
            return Ok();
        }
    }
}
