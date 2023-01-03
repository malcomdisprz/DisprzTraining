using DisprzTraining.Dtos;
using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public interface IAppointmentDAL
    {
        Task<Appointment> GetAppointmentByIdAsync(Guid id);
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<bool> AddAppointmentAsync(Appointment appointment);
        Task<bool> DeleteAppointmentAsync(Guid id);
        Task<bool> UpdateAppointmentAsync(Guid id, PutItemDto putItemDto);
    }
}