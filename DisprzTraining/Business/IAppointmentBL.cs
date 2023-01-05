using DisprzTraining.Dtos;
using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public interface IAppointmentBL
    {
        Task<Appointment> GetAppointmentByIdAsync(Guid id);
        Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date);
        Task<ItemDto> AddAppointmentAsync(PostItemDto postItemDto);
        Task<bool> DeleteAppointmentAsync(Guid id);
        Task<bool> UpdateAppointmentAsync(ItemDto putItemDto);
    }
}