using DisprzTraining.DataAccess;
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

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsBLAsync()
        {
            return  await _appointmentDAL.GetAllAppointmentsDALAsync();
        }
         public async Task<IEnumerable<Appointment>> GetAppointmentByDateBLAsync(DateTime Date)
        {
            return  await _appointmentDAL.GetAppointmentByDateDALAsync(Date);
        }
        public async Task<Appointment> GetAppointmentByEventBLAsync(string Event )
        {
            return  await _appointmentDAL.GetAppointmentByEventDALAsync(Event);
        }
        public async Task<Appointment> GetAppointmentByIdBLAsync(Guid id)
        {
            return  await _appointmentDAL.GetAppointmentByIdDALAsync(id);
        }
         public async Task<Appointment> CreateAppointmentBLAsync(Appointment appointment)
        {
            return  await _appointmentDAL.CreateAppointmentDALAsync(appointment);
        }
        public async Task<Appointment> UpdateAppointmentBLAsync(Appointment request)
        {
            return  await _appointmentDAL.UpdateAppointmentDALAsync(request);
        }
        public  Task DeleteAppointmentBLAsync(Guid id)
        {
            return _appointmentDAL.DeleteAppointmentDALAsync(id);
        }
    }
}


