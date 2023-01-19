using DisprzTraining.Models;
using DisprzTraining.Data;
using System.Linq;
using Newtonsoft.Json;
using DisprzTraining.CustomErrorCodes;
namespace DisprzTraining.DataAccess
{
    public class AppointmentsDAL : IAppointmentsDAL
    {
        // private bool CheckConflict(List<Appointment> list,AddNewAppointment data)
        // {
        //     if (DataStore.newList.Any())
        //     {
        //         foreach (var item in DataStore.newList)
        //         {
        //             if ((data.StartTime < item.EndTime) && (item.StartTime < data.EndTime))
        //             {
        //                 return false;
        //             }
        //         }
        //         return true;
        //     }
        //     return true;
        // }
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
        // private bool CheckUpdateConflict(Appointment data)
        // {

        //     foreach (var item in DataStore.newList)
        //     {
        //         if (item.Id == data.Id)
        //         {
        //             continue;
        //         }
        //         if ((data.StartTime < item.EndTime) && (item.StartTime < data.EndTime))
        //         {
        //             return false;
        //         }
        //     }
        //     return true;

        // }

        public bool CreateNewAppointments(AddNewAppointment data)
        {

            try
            {
                if (DataStore.dictionaryData.TryGetValue(data.Date, out List<Appointment> existingList))
                {

                    bool Isconflict = CheckConflict(existingList, data);
                    if (Isconflict == true)
                    {
                        existingList.Add(new Appointment()
                        {
                            Id = Guid.NewGuid(),
                            Date = data.Date,
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
                    DataStore.dictionaryData.Add(data.Date, new List<Appointment>{new Appointment()
                {
                        Id = Guid.NewGuid(),
                        Date = data.Date,
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
            catch (Exception e)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.invalidInputs));
            }
        }
        
        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {

            List<Appointment> existingList;
            if (DataStore.dictionaryData.TryGetValue(date, out existingList))
            {

                return existingList;

            }
            else
                return new List<Appointment>();

        }
        public List<Appointment> GetRangedList(DateTime date)
        {
            List<Appointment> value = new List<Appointment>();
            DateTime endRange = date.AddDays(7);
            var temp = DataStore.dictionaryData.SelectMany(x => x.Value);
            foreach (var item in temp)
            {
                if (item.EndTime > date && item.EndTime < endRange && item.EndTime > DateTime.Now)
                {
                    value.Add(item);
                }
            }
            return value;
        }
        public bool RemoveAppointmentsById(Guid id, DateTime date)
        {
            // try
            // {
            //     var remove = DataStore.newList.Single(r => r.Id == id);
            //     DataStore.newList.Remove(remove);
            //     return (true);

            // }
            // catch (Exception e)
            // {
            //     return (false);
            // }
            if (DataStore.dictionaryData.TryGetValue(date, out List<Appointment> list))
            {

                var remove = list.Find(r => r.Id == id);
                if (list.Contains(remove))
                {
                    list.Remove(remove);
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
            if (DataStore.dictionaryData.TryGetValue(data.Date, out List<Appointment> list))
            {
                var valueToBeUpdated = (from s in list where (s.Id == data.Id && s.Date == data.Date) select s).Single();
                bool Isconflict = CheckUpdateConflict(list, data);
                if (Isconflict)
                {
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
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.invalidDate));
            }
        }
    }
}
