using DisprzTraining.Business;
using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc;


namespace DisprzTraining.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentBL _appointmentBL;
        public AppointmentsController(IAppointmentBL appointmentBL)
        {
            _appointmentBL = appointmentBL;
        }


        /// <summary>
        /// Get all the appointments in a day.
        /// </summary>
        ///<remarks>
        /// Sample request:
        ///
        ///      day : "2023-01-23"(yyyy-mm-dd)
        ///
        /// </remarks>
        /// <response code="200">Returns list of appointment</response>
        /// <response code="404">No appointments found</response>
        //- GET /api/appointments/day

        [HttpGet("appointments/day")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AppointmentDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<>))]

        public async Task<IActionResult> GetByDay(DateTime day)
        {
            var appointments = await _appointmentBL.GetByDayAsync(day);
            return appointments.Any() ? Ok(appointments) : NotFound(new List<AppointmentDto>());
        }


        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointmentDto"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "title": "string",
        ///        "startTime": "2023-01-02T09:33:03.125",
        ///        "endTime": "2023-01-02T09:33:03.125"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created appointment</response>
        /// <response code="409">If there is a conflict</response>

        //- POST /api/appointments
        [HttpPost("appointments")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentDto))]
        // [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(string))]

        public async Task<IActionResult> Post(CreateAppointmentDto appointmentDto)
        {
            if (appointmentDto.StartTime >= appointmentDto.EndTime) return BadRequest();
            var newAppointment = await _appointmentBL.CreateAsync(appointmentDto);
            return newAppointment == null ? Conflict(new { message = $"There is a conflict"}) : CreatedAtAction(nameof(GetByDay), new { Id = newAppointment.Id }, newAppointment);
        }


        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="appointmentDto"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///        "id": "8d6812c7-348b-419f-b6f9-d626b6c1d363",
        ///        "title": "string",
        ///        "startTime": "2023-01-02T09:33:03.125",
        ///        "endTime": "2023-01-02T09:33:03.125"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the updated appointment</response>
        /// <response code="409">If there is a conflict</response>

        [HttpPut("appointments")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(AppointmentDto))]

        public async Task<IActionResult> Put(AppointmentDto appointmentDto)
        {
            if (appointmentDto.StartTime >= appointmentDto.EndTime) return BadRequest();
            var newAppointment = await _appointmentBL.UpdateAsync(appointmentDto);
            return newAppointment == null ? Conflict() : CreatedAtAction(nameof(GetByDay), new { Id = newAppointment.Id }, newAppointment);
        }

        /// <summary>
        /// Deletes a specific appointment.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///        id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///
        /// </remarks>
        /// <response code="204">Deletes an appointment successfully</response>
        /// <response code="404">Appointment is not found</response>
        // DELETE /api/appointments/{Id}
        [HttpDelete("appointments/{Id}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(AppointmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<>))]

        public async Task<IActionResult> Delete(Guid Id)
        {
            var boo = await _appointmentBL.Delete(Id);
            // return boo ? StatusCode(204, "Appointment deleted") : NotFound(new AppointmentDto());
            return boo ? StatusCode(204, "Appointment deleted") : NotFound(new AppointmentDto());
        }
    }
}
