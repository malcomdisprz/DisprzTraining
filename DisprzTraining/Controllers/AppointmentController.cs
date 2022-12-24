using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc;
using DisprzTraining.Business;
using System;

namespace DisprzTraining.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentBL _AppointmentBL;
        public AppointmentController(IAppointmentBL appointmentBL)
        {
            _AppointmentBL = appointmentBL;
        }

        [HttpGet("appointments")]
        public async Task<IActionResult> GetAllAppointmentsAsync()
        {
            var appointments = await _AppointmentBL.GetAllAppointmentsBLAsync();
            return (appointments == null) ? NotFound("No Event found") : Ok(appointments);
        }

        [HttpGet("appointments/Date")]
        public async Task<IActionResult> GetAppointmentByDateAsync(DateTime GivenDate)
        {
            var appointments = await _AppointmentBL.GetAppointmentByDateBLAsync(GivenDate);
            return (appointments == null)? NotFound("No Event found on the given date") : Ok(appointments);
        }

        [HttpGet("appointments/event")]
        public async Task<IActionResult> GetAppointmentByEventAsync(string Event)
        {
            var appointment = await _AppointmentBL.GetAppointmentByEventBLAsync(Event);
            return (appointment == null)? NotFound("Event not found") : Ok(appointment);
        }

        [HttpGet("appointments/event/{id}")]
        public async Task<IActionResult> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _AppointmentBL.GetAppointmentByIdBLAsync(id);
            return (appointment == null)? NotFound("Id not found") : Ok(appointment);
        }

        [HttpPost("appointments")]
        public async Task<IActionResult> CreateAppointmentAsync(Appointment appointment)
        {
            if (appointment.FromTime > appointment.ToTime)
            {
                return BadRequest("Invalid time interval");
            }
            else
            {
                var NewAppointment = await _AppointmentBL.CreateAppointmentBLAsync(appointment);
                return (NewAppointment == null) ? Conflict("Meeting Exixts in the given time") : Created("Created", NewAppointment);
            }
        }

        [HttpPut("appointments")]
        public async Task<IActionResult> UpdateAppointmentAsync(Appointment request)
        {
            if (request.FromTime > request.ToTime)
            {
                return BadRequest("Invalid time interval");
            }
            else
            {
                var NewAppointment = await _AppointmentBL.UpdateAppointmentBLAsync(request);
                return (NewAppointment == null)? Conflict("Meeting Exixts in the given time") : NoContent();
            }
        }

        [HttpDelete("appointments/event/{id}")]
        public async Task<IActionResult> DeleteAppointmentAsync(Guid id)
        {
            var appointment = _AppointmentBL.DeleteAppointmentBLAsync(id);
            return (appointment == null)? NotFound("Id not found") :NoContent();
        }
    }
}
