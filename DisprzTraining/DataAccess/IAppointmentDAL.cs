using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public interface IAppointmentDAL
    {
        Task<bool> Create(Appointment appointment);
        Task<List<Appointment>> GetByDay(DateTime day);
        Task<bool> Update(Appointment appointment);
        Task<bool> Delete(Guid Id);
    }
}