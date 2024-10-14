using Enplace.Service;

namespace Enplace.Service.DTO
{
    public class Notification
    {
        public required string Message { get; set; }
        public NotificationType Type { get; set; }
    }
}
