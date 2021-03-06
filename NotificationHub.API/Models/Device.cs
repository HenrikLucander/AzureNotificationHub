using NotificationHub.SharedLib.Models;

namespace NotificationHub.API.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public Platform Platform { get; set; }
        public string PnsToken { get; set; }
        public string RegistrationId { get; set; }

        public string Username { get; set; }
        public User User { get; set; }
    }
}
