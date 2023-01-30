using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc;
using DisprzTraining.Business;
using System;
using System.Net;

namespace DisprzTraining.Controllers
{
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentBL _AppointmentBL;
        public AppointmentController(IAppointmentBL appointmentBL)
        {
            _AppointmentBL = appointmentBL;
        }

        [HttpGet]
        [Route("appointments")]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAppointmentsAsync()
        {
            var appointments = await _AppointmentBL.GetAllAppointmentsBLAsync();
            return Ok(appointments);
        }

        [HttpGet]
        [Route("appointments/date")]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAppointmentByDateAsync(DateTime Date)
        {
            var appointments = await _AppointmentBL.GetAppointmentByDateBLAsync(Date);
            return Ok(appointments);
        }

        [HttpGet]
        [Route("appointments/event")]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAppointmentByEventAsync(string Event)
        {
            var appointment = await _AppointmentBL.GetAppointmentByEventBLAsync(Event);
            return (appointment == null) ? NotFound("No event found ") : Ok(appointment);
        }

        [HttpGet]
        [Route("appointments/event/{id}")]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _AppointmentBL.GetAppointmentByIdBLAsync(id);
            return (appointment == null) ? NotFound("Id not found") : Ok(appointment);
        }

        [HttpPost]
        [Route("appointments")]
        [ProducesResponseType(typeof(Appointment), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAppointmentAsync(Appointment appointment)
        {
            if (appointment.FromTime >= appointment.ToTime)
            {
                return BadRequest("Invalid time interval");
            }
            else
            {
                var NewAppointment = await _AppointmentBL.CreateAppointmentBLAsync(appointment);
                return (NewAppointment == null) ? Conflict("Meeting Exixts in the given time") : Created("Created", NewAppointment);
            }
        }

        [HttpPut]
        [Route("appointments")]
        public async Task<IActionResult> UpdateAppointmentAsync(Appointment request)
        {
            if (request.FromTime >= request.ToTime)
            {
                return BadRequest("Invalid time interval");
            }
            else
            {
                var NewAppointment = await _AppointmentBL.UpdateAppointmentBLAsync(request);
                return (NewAppointment == null) ? Conflict("Meeting Exixts in the given time") : NoContent();
            }
        }

        [HttpDelete]
        [Route("appointments/event/{id}")]
        public async Task<IActionResult> DeleteAppointmentAsync(Guid id)
        {
            var appointment = _AppointmentBL.DeleteAppointmentBLAsync(id);
            return (appointment == null) ? NotFound("Id not found") : NoContent();
        }
    }
}
