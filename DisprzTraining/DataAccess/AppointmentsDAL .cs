using DisprzTraining.Models;
using DisprzTraining.Data;
using System.Linq;
using Newtonsoft.Json;
using DisprzTraining.CustomErrorCodes;
namespace DisprzTraining.DataAccess
{
    public class AppointmentsDAL : IAppointmentsDAL
    {

        private bool CheckConflict(List<Appointment> list, AddNewAppointment data)
        {
            foreach (var item in list)
            {
                if ((data.StartTime < item.EndTime) && (item.StartTime < data.EndTime))
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckUpdateConflict(List<Appointment> list, Appointment data)
        {

            foreach (var item in list)
            {
                if (item.Id == data.Id)
                {
                    continue;
                }
                if ((data.StartTime < item.EndTime) && (item.StartTime < data.EndTime))
                {
                    return false;
                }
            }
            return true;

        }

        public bool CreateNewAppointments(AddNewAppointment data)
        {

            DateTime date = (data.Date).Date;
            if (DataStore.dictionaryData.TryGetValue(date, out List<Appointment> existingList))
            {

                bool noConflict = CheckConflict(existingList, data);
                if (noConflict == true)
                {
                    existingList.Add(new Appointment()
                    {
                        Id = Guid.NewGuid(),
                        Date = date,
                        Title = data.Title,
                        Description = data.Description,
                        Type = data.Type,
                        StartTime = data.StartTime,
                        EndTime = data.EndTime,
                    });

                    existingList.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                DataStore.dictionaryData.Add(date, new List<Appointment>{new Appointment()
                {
                        Id = Guid.NewGuid(),
                        Date = date,
                        Title = data.Title,
                        Description = data.Description,
                        Type = data.Type,
                        StartTime = data.StartTime,
                        EndTime = data.EndTime,
                }
                });
                return true;
            }
        }

        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            DateTime convertedDate = date.Date;
            if (DataStore.dictionaryData.TryGetValue(convertedDate, out List<Appointment> existingList))
            {
                return existingList;
            }
            else
                return new List<Appointment>();

        }
        public List<Appointment> GetRangedList(DateTime date)
        {
            List<Appointment> value = new List<Appointment>();
            DateTime convertedDate = date.Date;
            DateTime keyDate = convertedDate;
            for (int i = 0; i < 7; i++)
            {
                if (DataStore.dictionaryData.TryGetValue(keyDate, out List<Appointment> existingList))
                {
                    foreach (var item in existingList)
                    {
                        if (item.EndTime > convertedDate && item.EndTime > DateTime.Now)
                        {
                            value.Add(item);
                        }
                    }
                }
                keyDate = keyDate.AddDays(1);
            }
            return value;
        }
        public bool RemoveAppointmentsById(Guid id, DateTime date)
        {
            DateTime convertedDate = date.Date;
            if (DataStore.dictionaryData.TryGetValue(convertedDate, out List<Appointment> list))
            {
                var remove = list.Find(r => r.Id == id);
                if (remove != null)
                {
                    list.Remove(remove);
                    if (DataStore.dictionaryData[convertedDate].Count == 0)
                        DataStore.dictionaryData.Remove(convertedDate);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UpdateAppointmentById(Appointment data)
        {
            DateTime date = (data.Date).Date;
                 
            // if (DataStore.dictionaryData.TryGetValue(date, out List<Appointment> list))
            // {
                List<Appointment>list=DataStore.dictionaryData[date];
                bool noConflict = CheckUpdateConflict(list, data);
                if (noConflict)
                {
                    var valueToBeUpdated = list.Find(x => x.Id == data.Id);
                    valueToBeUpdated.Title = data.Title;
                    valueToBeUpdated.Description = data.Description;
                    valueToBeUpdated.Type = data.Type;
                    valueToBeUpdated.StartTime = data.StartTime;
                    valueToBeUpdated.EndTime = data.EndTime;
                    list.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
                    return true;
                }
                else
                {
                    return false;
                }
            // }
            // else
            // {
            //     throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.invalidDate));
            // }
        }
        public bool CheckForId(Guid id, DateTime date)
        {
            if (DataStore.dictionaryData.TryGetValue(date, out List<Appointment> list))
            {
                if (DataStore.dictionaryData[date].Exists(x => x.Id == id))
                {
                    return true;
                }
                else return false;
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.invalidDate));
            }
        }
    }
}
