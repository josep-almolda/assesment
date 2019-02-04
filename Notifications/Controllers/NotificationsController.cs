using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notifications.Common.Interfaces;
using Notifications.Common.Models;

namespace Notifications.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsService _notificationsService;

        public NotificationsController(INotificationsService notificationsService)
        {
            this._notificationsService = notificationsService;
        }

        [Route("")]
        [HttpGet]
        public IReadOnlyCollection<NotificationModel> Get()
        {
            return _notificationsService.GetAllNotifications();
        }

        [Route("")]
        [HttpPost]
        public IActionResult Post([FromBody]NotificationModel notification)
        {
            try
            {
                if (notification == null)
                {
                    return BadRequest("notification is null");
                }

                if (notification.Type == null)
                {
                    return BadRequest("Notification type unrecognised");
                }

                this._notificationsService.CreateNotification(notification);

                return Created("api/Notifications", notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error creating the notification: {ex.Message}");
            }
        }
    }
}