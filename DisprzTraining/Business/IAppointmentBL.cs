using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public interface IAppointmentBL
    {
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto appointmentDto);
        Task<List<AppointmentDto>> GetByDayAsync(DateTime day);
        Task<AppointmentDto> UpdateAsync(AppointmentDto appointmentDto);
        Task<bool> Delete(Guid Id);
    }
}