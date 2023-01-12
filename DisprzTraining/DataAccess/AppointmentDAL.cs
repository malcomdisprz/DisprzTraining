using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public class AppointmentDAL : IAppointmentDAL
    {

        private readonly static List<Appointment> appointments = new List<Appointment>(){
            new Appointment{
                Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d361"),
                Title = "M1",
                StartTime = new DateTime(2022, 12, 30, 5, 10, 20),
                EndTime = new DateTime(2022, 12, 30, 6, 10, 20)
            },
            new Appointment{
                Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d362"),
                Title = "M2",
                StartTime = new DateTime(2022, 12, 30, 7, 10, 20),
                EndTime = new DateTime(2022, 12, 30, 8, 10, 20)
            },
            new Appointment{
                Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d363"),
                Title = "M3",
                StartTime = new DateTime(2022, 12, 31, 5, 10, 20),
                EndTime = new DateTime(2022, 12, 31, 8, 10, 20)
            },
            new Appointment{
                Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d364"),
                Title = "M4",
                StartTime = new DateTime(2022, 12, 31, 10, 10, 20),
                EndTime = new DateTime(2022, 12, 31, 11, 10, 20)
            },
            new Appointment{
                Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d365"),
                Title = "M5",
                StartTime = new DateTime(2022, 12, 27, 11, 10, 20),
                EndTime = new DateTime(2022, 12, 27, 11, 50, 20)
            }
        };
        public Task<bool> Create(Appointment appointment)
        {

            var newAppointments = appointments.Where(s => (s.StartTime.Date == appointment.StartTime.Date));

            if (newAppointments.Count() == 0)
            {
                appointments.Add(appointment);
                return Task.FromResult(true);
            }

            var check = newAppointments.Any(
                s => (
                        ((s.StartTime >= appointment.StartTime) && (s.StartTime <= appointment.EndTime))
                        || ((s.EndTime >= appointment.StartTime) && (s.EndTime <= appointment.EndTime))
                        || ((appointment.StartTime >= s.StartTime) && (appointment.StartTime <= s.EndTime))
                        || ((appointment.EndTime >= s.StartTime) && (appointment.EndTime <= s.EndTime))
                    )
                );

            if (check)
            {
                return Task.FromResult(false);
            }
            else
            {
                appointments.Add(appointment);
                return Task.FromResult(true);
            }

        }

        public async Task<bool> Update(Appointment appointment)
        {
            var existingAppointment = appointments.Find(s => s.Id == appointment.Id);
            appointments.Remove(existingAppointment);
            var check = await Create(appointment);
            if (check == true)
            {
                return true;
            }
            else
            {
                appointments.Add(existingAppointment);
                return false;
            }
        }

        public Task<bool> Delete(Guid Id)
        {
            var appointment = appointments.Find(s => s.Id == Id);
            if (appointment is null)
            {
                return Task.FromResult(false);
            }
            appointments.Remove(appointment);
            return Task.FromResult(true);
        }

        public Task<List<Appointment>> Get(Request request)
        {
            if (request.Day != DateTime.MinValue && request.Month == DateTime.MinValue)
            {
                int year = request.Day.ToLocalTime().Year;
                int month = request.Day.ToLocalTime().Month;
                int day = request.Day.ToLocalTime().Day;

                var newAppointments = appointments.Where(s => (s.StartTime.Year == year) && (s.StartTime.Month == month) && (s.StartTime.Day == day)).ToList();
                return Task.FromResult(newAppointments);
            }
            else if (request.Day == DateTime.MinValue && request.Month != DateTime.MinValue)
            {
                int year = request.Month.ToLocalTime().Year;
                int month = request.Month.ToLocalTime().Month;
                
                var newAppointments = appointments.Where(s => (s.StartTime.Year == year) && (s.StartTime.Month == month)).ToList();
                return Task.FromResult(newAppointments);
            }
            else{
                return Task.FromResult( new List<Appointment>());
            }
        }

    }
}
