using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public interface IAppointmentBL
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsBLAsync();
        Task<IEnumerable<Appointment>> GetAppointmentByDateBLAsync(DateTime Date);
        Task<Appointment> GetAppointmentByEventBLAsync(string Event);
        Task<Appointment> GetAppointmentByIdBLAsync(Guid id);
        Task<Appointment> CreateAppointmentBLAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentBLAsync(Appointment request);
        Task DeleteAppointmentBLAsync(Guid id);
    }
}