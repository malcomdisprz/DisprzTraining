using DisprzTraining.Models;
using DisprzTraining.Data;
namespace DisprzTraining.DataAccess
{
    public class AppointmentsDAL : IAppointmentsDAL
    {

        public Task<bool> CreateNewAppointments(AddNewAppointment data)
        {
            try
            {
                DataStore.newList.Add(new Appointment()
                {      
                Id=Guid.NewGuid(),
                Date=data.Date,
                Title=data.Title,
                Description=data.Description,
                StartTime=data.StartTime,
                EndTime =data.EndTime,
                });
                return Task.FromResult(true);
            }
            catch (Exception e)
            {

                return Task.FromResult(false);
            }

        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            return DataStore.newList;
        }
        public async Task<List<Appointment>> GetAppointmentsByDate(string date)
        {
             return DataStore.newList.FindAll(x =>x.Date.Contains(date));
             
        }
        public Task<bool> RemoveAppointmentsById(Guid id)
        {
            try{
                // DataStore.newList.Any(c => c.Id == id);
                var remove = DataStore.newList.Single(r => r.Id == id);
                DataStore.newList.Remove(remove);
                return Task.FromResult(true);
            
            }
             catch(Exception e)
            {
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateAppointmentById(Appointment data)
        {
            try
            {
                var update = from s in DataStore.newList where (s.Id == data.Id&&s.Date==data.Date) select s;
                update.Single().Title = data.Title;
                update.Single().Description = data.Description;
                update.Single().StartTime = data.StartTime;
                update.Single().EndTime = data.EndTime;
                return Task.FromResult(true);
            }
            catch(Exception e)
            {
                return Task.FromResult(false);
            }
            
        }


    }
}