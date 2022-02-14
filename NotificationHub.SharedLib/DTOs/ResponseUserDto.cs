using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationHub.SharedLib.DTOs
{
    public class ResponseUserDto
    {
        public string Username { get; set; }
        public List<ResponseDeviceDto> Devices { get; set; } = new List<ResponseDeviceDto>();

        public ResponseUserDto(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            Username = username;
        }
    }
}
