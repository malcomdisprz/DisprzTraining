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
                    StartTime = appointmentDto.StartTime,
                    EndTime = appointmentDto.EndTime
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
                StartTime = appointmentDto.StartTime,
                EndTime = appointmentDto.EndTime
            };
            var check = await _appointmentDAL.Update(appointment);
            return check ? appointment.AsDto() : null;
        }

        public Task<bool> Delete(Guid Id)
        {
            var check = _appointmentDAL.Delete(Id);
            return check;
        }

        public async Task<List<AppointmentDto>> GetByDayAsync(DateTime day)
        {
            var appointmentDtos = (await _appointmentDAL.GetByDay(day))
                        .Select(appointment => appointment.AsDto()).ToList();
            return appointmentDtos;
        }
    }
}