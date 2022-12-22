using DisprzTraining.Models;
using Microsoft.AspNetCore.Mvc;
using DisprzTraining.Business;
using System.ComponentModel.DataAnnotations;

namespace DisprzTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentsBL _appointmentsBL;

        public AppointmentsController(IAppointmentsBL appointmentsBL)
        {
            _appointmentsBL =appointmentsBL;
        }

        //design - GET /api/appointments
        //- POST /api/appointments
        //- DELETE /api/appointments
        // public static List<Appointment> list=new List<Appointment>();
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
         [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateAppointment (AddNewAppointment data)
        {
             bool postCreated=await _appointmentsBL.CreateNewAppointment(data);
             if(postCreated==true)
             {
                return Created(" ",postCreated);
             }
             else
             {
                return Conflict();
             }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Appointment>>> GetAppointments()
        {
           return Ok(await _appointmentsBL.GetAllAddedAppointments());
        }

        
        [HttpGet("{date}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentsForDay([Required]string date)
        {
           return Ok(await _appointmentsBL.GetAppointments(date));
        }
        

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Remove(Guid id)
        {
            bool IdIsThere=await _appointmentsBL.RemoveAppointments(id);
            if (IdIsThere==true)
            {
                     return Ok(IdIsThere);
            }
            else
            {
            return NotFound("enter Valid-id");
            }  
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Update([FromBody]Appointment data)
        {
            bool updated=await _appointmentsBL.UpdateAppointments(data);
            if(updated==true)
            {
                return Ok(updated);
            }
            else
            {
            return Conflict();
            }
        }

    }
}
