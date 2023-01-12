using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public interface IAppointmentDAL
    {
        Task<bool> Create(Appointment appointment);
        Task<List<Appointment>> Get( Request request);
        Task<bool> Update(Appointment appointment);
        Task<bool> Delete(Guid Id);
    }
}