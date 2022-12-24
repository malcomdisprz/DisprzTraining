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
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsDALAsync()
        {
            return appointments.Any() ? await Task.FromResult(appointments) : null;
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentByDateDALAsync(DateTime GivenDate)
        {
            var appointment = appointments.FindAll(Date => Date.FromTime.ToShortDateString() == GivenDate.ToShortDateString());  
            return appointment.Any() ? await Task.FromResult(appointment) : null;
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentByEventDALAsync(string Event)
        {
            var appointment = appointments.FindAll(Events => Events.EventName.ToLower().Contains(Event.ToLower())).ToList();
            return appointment.Any() ? await Task.FromResult(appointment) : null;
        }
        public async Task<Appointment> GetAppointmentByIdDALAsync(Guid id)
        {
            var appointment = appointments.Find(findid => findid.Id == id);
            return (appointment==null) ? null : await Task.FromResult(appointment);
        }
        public async Task<Appointment> CreateAppointmentDALAsync(Appointment appointment)
        {
            var newappointment = appointments.Find(find => (find.FromTime >= appointment.FromTime && find.FromTime < appointment.ToTime ) ||
                                                           (find.ToTime> appointment.FromTime &&  find.ToTime <= appointment.ToTime) ||
                                                           (find.FromTime<appointment.FromTime && find.ToTime>appointment.ToTime));
            if (newappointment == null)
            {
                appointments.Add(appointment);
                return await Task.FromResult(appointment);
            }
            return null;
        }
        public async Task<Appointment> UpdateAppointmentDALAsync(Appointment request)
        {
            var FindId = appointments.Find(findid => findid.Id == request.Id);
            if (FindId != null)
            {
                var newappointment = appointments.Find(find => (find.FromTime >= request.FromTime && find.FromTime < request.ToTime ) ||
                                                               (find.ToTime> request.FromTime &&  find.ToTime <= request.ToTime) ||
                                                               (find.FromTime<request.FromTime && find.ToTime>request.ToTime));
                if (newappointment == null)
                {
                    FindId.EventName = request.EventName;
                    FindId.FromTime = request.FromTime;
                    FindId.ToTime = request.ToTime;
                    return await Task.FromResult(request);
                }
                return null;
            }
            return null;
        }
        public Task DeleteAppointmentDALAsync(Guid id)
        {
            var appointment = appointments.Find(findid => findid.Id == id);
            if (appointment != null)
            {
                appointments.Remove(appointment);
                return Task.CompletedTask;
            }
            return null;
        }
    }
}