using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationHub.SharedLib.DTOs
{
    public class SendNotificationDto
    {
        [Required]
        public string Username { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
