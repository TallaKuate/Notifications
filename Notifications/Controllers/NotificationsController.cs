using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        //[Route("")]
        //[HttpGet]
        //public IReadOnlyCollection<NotificationModel> Get()
        //{
        //    return _notificationsService.GetAllNotifications();
        //}

        [HttpGet("user/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IReadOnlyCollection<NotificationModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserCancelledAppointmentNotification(Guid id)
        {
            var result = await _notificationsService.GetUserCancelledAppointmentNotifications(id).ConfigureAwait(false);
            if(result == null || !result.Any())
                return NotFound(result);         
            return Ok(result);         
        }

        /// <summary>
        /// Add User Notification
        /// </summary>
        /// <param name="request">
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(NotificationModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUserNotification([FromBody] NotificationEventModel request)
        {
            var result = await _notificationsService.AddUserNotification(request).ConfigureAwait(false);  
            if(result == null || result.Id == null || result.Id == default)
                return NotFound(result);  
            return Ok(result);
        }
    }
}