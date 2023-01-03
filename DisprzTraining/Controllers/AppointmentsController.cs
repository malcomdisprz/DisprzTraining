using DisprzTraining.Business;
using DisprzTraining.Dtos;
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

        [HttpGet("appointments/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAppointmentByIdAsync(Guid id)
        {
            var item = (await _appointmentBL.GetAppointmentByIdAsync(id));

            return item != null ? Ok(item) : NotFound();
        }

        [HttpGet("appointments/date")]
        [ActionName(nameof(GetAppointmentsByDateAsync))]
        [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAppointmentsByDateAsync(DateTime date)
        {
            var item = (await _appointmentBL.GetAppointmentsByDateAsync(date))
                           .Select(item => item.AsDto()).ToList();
            List<Appointment> notFound = new();
            return item.Any() ? Ok(item) : NotFound(notFound);
        }


        [HttpPost("appointments")]
        [ProducesResponseType(typeof(ItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddAppointmentAsync(PostItemDto postItemDto)
        {
            if (postItemDto.startDate >= postItemDto.endDate)
            {
                return BadRequest("Invalid Time");
            }
            var res = (await _appointmentBL.AddAppointmentAsync(postItemDto));
            return res != null ? CreatedAtAction(nameof(GetAppointmentsByDateAsync), new { id = res.id }, res) : Conflict();
        }

        [HttpDelete("appointments/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAppointmentAsync(Guid id)
        {
            var res = await _appointmentBL.DeleteAppointmentAsync(id);

            return res ? NoContent() : NotFound();
        }

        [HttpPut("appointments/{id}")]
        public async Task<ActionResult> UpdateAppointmentAsync(Guid id, PutItemDto putItemDto)
        {
            try
            {
                var appointmentToUpdate = await _appointmentBL.GetAppointmentByIdAsync(id);

                if (appointmentToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");

                if (putItemDto.startDate >= putItemDto.endDate)
                {
                    return BadRequest("Invalid Time");
                }

                var res = await _appointmentBL.UpdateAppointmentAsync(id, putItemDto);

                return res ? Ok() : Conflict();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        //design - GET /api/appointments
        //- POST /api/appointments
        //- DELETE /api/appointments

        //refer hello world controller for BL & DAL logic 

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GerAppointments()
        //{
        //    return Ok();
        //}

    }
}
