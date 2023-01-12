using DisprzTraining.DataAccess;
using DisprzTraining.Models;

namespace DisprzTraining.Business
{
    public class AppointmentBL : IAppointmentBL
    {

        private readonly IAppointmentDAL _appointmentDAL;

        public AppointmentBL(IAppointmentDAL appointmentDAL)
        {
            _appointmentDAL = appointmentDAL;
        }

        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto appointmentDto)
        {
            Appointment appointment = new()
            {
                Id = Guid.NewGuid(),
                Title = appointmentDto.Title,
                StartTime = appointmentDto.StartTime.ToLocalTime(),
                EndTime = appointmentDto.EndTime.ToLocalTime()
            };
            var check = await _appointmentDAL.Create(appointment);
            return check ? appointment.AsDto() : null;
        }

        public async Task<AppointmentDto> UpdateAsync(AppointmentDto appointmentDto)
        {
            Appointment appointment = new()
            {
                Id = appointmentDto.Id,
                Title = appointmentDto.Title,
                StartTime = appointmentDto.StartTime.ToLocalTime(),
                EndTime = appointmentDto.EndTime.ToLocalTime()
            };
            var check = await _appointmentDAL.Update(appointment);
            return check ? appointment.AsDto() : null;
        }

        public Task<bool> Delete(Guid Id)
        {
            var check = _appointmentDAL.Delete(Id);
            return check;
        }

        public async Task<IDictionary<int, List<AppointmentDto>>> GetAsync(Request request)
        {

            IDictionary<int, List<AppointmentDto>> appointments = new Dictionary<int, List<AppointmentDto>>();

            var appointmentDtos = (await _appointmentDAL.Get(request))
                    .Select(appointment => appointment.AsDto()).ToList();

            if (request.Day != DateTime.MinValue && request.Month == DateTime.MinValue)
            {
                foreach (var item in appointmentDtos)
                {
                    int id = item.StartTime.Hour;
                    if (appointments.ContainsKey(id))
                    {
                        appointments[id].Add(item);
                    }
                    else
                    {
                        appointments.Add(id, new List<AppointmentDto>());
                        appointments[id].Add(item);
                    }
                }
                return appointments;
            }

            else if (request.Day == DateTime.MinValue && request.Month != DateTime.MinValue)
            {
                foreach (var item in appointmentDtos)
                {
                    int id = item.StartTime.Day;
                    if (appointments.ContainsKey(id))
                    {
                        appointments[id].Add(item);
                    }
                    else
                    {
                        appointments.Add(id, new List<AppointmentDto>());
                        appointments[id].Add(item);
                    }
                }
                return appointments;
            }
            
            else{
                return appointments;
            }
        }

    }
}