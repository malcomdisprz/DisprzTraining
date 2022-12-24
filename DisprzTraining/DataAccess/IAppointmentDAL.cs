using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public interface IAppointmentDAL
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsDALAsync();
        Task<IEnumerable<Appointment>> GetAppointmentByDateDALAsync(DateTime GivenDate);
        Task<IEnumerable<Appointment>> GetAppointmentByEventDALAsync(string Event);
        Task<Appointment> GetAppointmentByIdDALAsync(Guid id);
        Task<Appointment> CreateAppointmentDALAsync(Appointment appointment);
        Task<Appointment> UpdateAppointmentDALAsync(Appointment request);
        Task DeleteAppointmentDALAsync(Guid id);
    }
}
