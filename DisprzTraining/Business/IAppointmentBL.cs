using System.Collections;
using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public interface IAppointmentBL
    {
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto appointmentDto);
        Task<IDictionary<int, List<AppointmentDto>>> GetAsync(Request request);
        Task<AppointmentDto> UpdateAsync(AppointmentDto appointmentDto);
        Task<bool> Delete(Guid Id);
    }
}
