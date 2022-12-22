using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public interface IAppointmentsBL 
    {
        Task<bool> CreateNewAppointment(AddNewAppointment data);

        Task<List<Appointment>> GetAllAddedAppointments();
       
        Task<List<Appointment>> GetAppointments(string date);

        Task<bool> RemoveAppointments(Guid id);

        Task<bool> UpdateAppointments(Appointment data);
    }
}