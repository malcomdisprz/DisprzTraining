using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{


    public interface IAppointmentsDAL
    {
        bool CreateNewAppointments(AddNewAppointment data);
        List<Appointment> GetAppointmentsByDate(DateTime date);

        List<Appointment> GetRangedList(DateTime date);
        bool RemoveAppointmentsById(Guid id, DateTime date);
        bool UpdateAppointmentById(Appointment data);

    }
}