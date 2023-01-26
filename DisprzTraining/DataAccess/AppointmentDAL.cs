using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public class AppointmentDAL : IAppointmentDAL
    {
        private static List<Appointment> appointments = new List<Appointment>()
        {
            new Appointment(){Id=Guid.NewGuid(),EventName="Daily Scrum",FromTime=new DateTime(2022,12,14,13,30,00),ToTime= new DateTime(2022,12,14,15,00,00)},
            new Appointment(){Id=Guid.NewGuid(),EventName="Meeting with EM",FromTime=new DateTime(2022,12,14,17,00,00),ToTime= new DateTime(2022,12,14,18,00,00)},
            new Appointment(){Id=Guid.NewGuid(),EventName="Meeting",FromTime=new DateTime(2022,12,15,17,00,00),ToTime= new DateTime(2022,12,15,18,00,00)}
        };
        public Boolean CheckConflict(Appointment appointment)
        {
            var newappointment = appointments.FindAll(x => (x.FromTime >= appointment.FromTime && x.FromTime < appointment.ToTime) ||
                                                           (x.ToTime > appointment.FromTime && x.ToTime <= appointment.ToTime) ||
                                                           (x.FromTime < appointment.FromTime && x.ToTime > appointment.ToTime));
            if (newappointment.Any())
            {
                var newevent = newappointment.FindAll(x => x.Id != appointment.Id);
                if (newevent.Any())
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsDALAsync()
        {
            return await Task.FromResult(appointments);
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentByDateDALAsync(DateTime Date)
        {
            var appointment = appointments.FindAll(x => x.FromTime.ToShortDateString() == Date.ToShortDateString()).OrderBy(s => s.FromTime).ToList();
            return appointment.Any() ? await Task.FromResult(appointment) : null;
        }
        public async Task<Appointment> GetAppointmentByEventDALAsync(string Event)
        {
            var appointment = appointments.Find(x => x.EventName == Event);
            return (appointment != null) ? await Task.FromResult(appointment) : null;
        }
        public async Task<Appointment> GetAppointmentByIdDALAsync(Guid id)
        {
            var appointment = appointments.Find(x => x.Id == id);
            return (appointment == null) ? null : await Task.FromResult(appointment);
        }
        public async Task<Appointment> CreateAppointmentDALAsync(Appointment appointment)
        {
            var newappointment = CheckConflict(appointment);
            if (newappointment == false)
            {
                appointments.Add(appointment);
                return await Task.FromResult(appointment);
            }
            return null;
        }
        public async Task<Appointment> UpdateAppointmentDALAsync(Appointment request)
        {
            var appointment = appointments.Find(x => x.Id == request.Id);
            if (appointment != null)
            {
                var newappointment = CheckConflict(request);
                if (newappointment == false)
                {
                    appointment.EventName = request.EventName;
                    appointment.FromTime = request.FromTime;
                    appointment.ToTime = request.ToTime;
                    return await Task.FromResult(request);
                }
                return null;
            }
            return null;
        }
        public Task DeleteAppointmentDALAsync(Guid id)
        {
            var appointment = appointments.Find(x => x.Id == id);
            if (appointment != null)
            {
                appointments.Remove(appointment);
                return Task.CompletedTask;
            }
            return null;
        }
    }
}