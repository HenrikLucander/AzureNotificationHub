using NotificationHub.SharedLib.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationHub.SharedLib.DTOs
{
    public class SignInUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public Guid? DeviceId { get; set; }

        [Required]
        public Platform? Platform { get; set; }

        [Required]
        public string PnsToken { get; set; }    // Called GcmRegistrationId (Android), DeviceToken (iOS) and ChannelUri (UWP) 
    }
}
