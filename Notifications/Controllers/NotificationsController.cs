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
        public IActionResult Post([FromBody]EventModel eventModel)
        {
            try
            {
                if (eventModel == null)
                {
                    return BadRequest("notification is null");
                }

                this._notificationsService.CreateNotification(eventModel);

                return Created("api/Notifications", eventModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error creating the notification: {ex.Message}");
            }
        }
    }
}