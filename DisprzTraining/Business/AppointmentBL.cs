using DisprzTraining.DataAccess;
using DisprzTraining.Dtos;
using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public class AppointmentBL : IAppointmentBL
    {
        private readonly IAppointmentDAL _appointmentDAL;
        public AppointmentBL(IAppointmentDAL appointmentDAL)
        {
            _appointmentDAL = appointmentDAL;
        }


        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            var res = await _appointmentDAL.GetAppointmentByIdAsync(id);
            if (res != null)
            {
                return res;
            }
            return null;
        }
        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            var res = await _appointmentDAL.GetAppointmentsByDateAsync(date);
            if (res != null)
            {
                return res;
            }
            return null;
        }

        public async Task<ItemDto> AddAppointmentAsync(PostItemDto postItemDto)
        {
            Appointment item = new()
            {
                id = Guid.NewGuid(),
                startDate = postItemDto.startDate,
                endDate = postItemDto.endDate,
                appointment = postItemDto.appointment
            };
            var check = await _appointmentDAL.AddAppointmentAsync(item);
            if (check)
            {
                return item.AsDto();
            }
            return null;
        }



        public async Task<bool> DeleteAppointmentAsync(Guid id)
        {
            var res = await _appointmentDAL.DeleteAppointmentAsync(id);
            if (res)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAppointmentAsync(Guid id, PutItemDto putItemDto)
        {
            var check = await _appointmentDAL.UpdateAppointmentAsync(id, putItemDto);
            if (check)
            {
                return true;
            }
            return false;
        }
    }
}


