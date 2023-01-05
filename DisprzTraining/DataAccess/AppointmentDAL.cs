using DisprzTraining.Dtos;
using DisprzTraining.Models;

namespace DisprzTraining.DataAccess
{
    public class AppointmentDAL : IAppointmentDAL
    {
        public static List<Appointment> allAppointments = new(){
            new Appointment(){id = Guid.NewGuid(), startDate = new DateTime(2023,1,1,6,10,0), endDate = new DateTime(2023,1,1,6,30,0), appointment = "TownHall"},
            new Appointment(){id = Guid.NewGuid(), startDate = new DateTime(2023,1,2, 9, 30, 0), endDate = new DateTime(2023,1,2,10,15,0), appointment = "LeaderShip"},
            new Appointment(){id = Guid.NewGuid(), startDate = new DateTime(2023,1,3, 11, 0, 0), endDate = new DateTime(2023,1,3,11,30,0), appointment = "standup"},

        };


        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            var item = allAppointments.Where(x => x.id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            var item = allAppointments.Where(x => x.startDate.Day == date.Day && x.startDate.Month == date.Month && x.startDate.Year == date.Year).ToList();
            return await Task.FromResult(item);
        }

        public async Task<bool> AddAppointmentAsync(Appointment appointment)
        {
            var exist = allAppointments.Any(x => (appointment.startDate > x.startDate && appointment.startDate < x.endDate) ||
                                                 (appointment.endDate > x.startDate && appointment.endDate < x.endDate) ||
                                                 (appointment.startDate <= x.startDate && appointment.endDate >= x.endDate)
                                            );
            if (!exist)
            {
                allAppointments.Add(appointment);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteAppointmentAsync(Guid id)
        {
            var item = allAppointments.Where(x => x.id == id).SingleOrDefault();
            if (item != null)
            {
                allAppointments.Remove(item);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> UpdateAppointmentAsync(ItemDto putItemDto)
        {
            var exist = allAppointments.Any(x => x.id != putItemDto.id &&
                                                 ((putItemDto.startDate > x.startDate && putItemDto.startDate < x.endDate) ||
                                                 (putItemDto.endDate > x.startDate && putItemDto.endDate < x.endDate) ||
                                                 (putItemDto.startDate <= x.startDate && putItemDto.endDate >= x.endDate))
                                            );
            if (exist)
            {
                return await Task.FromResult(false);
            }
            var appointmentToUpdate = allAppointments.Where(x => x.id == putItemDto.id).SingleOrDefault();
            appointmentToUpdate.startDate = putItemDto.startDate;
            appointmentToUpdate.endDate = putItemDto.endDate;
            appointmentToUpdate.appointment = putItemDto.appointment;
            return await Task.FromResult(true);
        }
    }
}

