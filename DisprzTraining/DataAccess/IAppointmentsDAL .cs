using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{

   
    public interface IAppointmentsDAL 
    {
         Task<bool> CreateNewAppointments(AddNewAppointment data);
         Task<List<Appointment>> GetAllAppointments();
         Task<List<Appointment>> GetAppointmentsByDate(string date);

         Task<bool> RemoveAppointmentsById(Guid id);
         Task<bool> UpdateAppointmentById(Appointment data);

    }
}