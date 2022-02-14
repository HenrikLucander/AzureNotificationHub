using NotificationHub.SharedLib.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationHub.API.Services
{
    public interface INotificationHubService
    {
        Task<string> CreateRegistration(SignInUserDto signInDeviceDto);
        Task<string> UpdateRegistration(SignInUserDto signInDeviceDto, string registrationId);
        Task DeleteRegistration(string registrationId);
        Task SendNotification(string message, string tag);
    }
}
