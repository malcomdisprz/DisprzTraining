using DisprzTraining.Models;
using DisprzTraining.DataAccess;
using DisprzTraining.Data;
using System.Globalization;
using Newtonsoft.Json;
using DisprzTraining.CustomErrorCodes;
namespace DisprzTraining.Business
{
    public class AppointmentsBL : IAppointmentsBL
    {
        private readonly IAppointmentsDAL _appointmentsDAL;

        public AppointmentsBL(IAppointmentsDAL appointmentsDAL)
        {
            _appointmentsDAL = appointmentsDAL;
        }

        public bool CreateNewAppointment(AddNewAppointment data)
        {
            CheckInputTime(data);

            return (_appointmentsDAL.CreateNewAppointments(data));

        }

        public List<Appointment> GetAppointmentsForSelectedDate(DateTime date)
        {

            return _appointmentsDAL.GetAppointmentsByDate(date);

        }

        public List<Appointment> GetRangedList(DateTime date)
        {
            return _appointmentsDAL.GetRangedList(date);
        }

        public bool RemoveAppointments(Guid id, DateTime date)
        {
            return _appointmentsDAL.RemoveAppointmentsById(id, date);
        }
        
        public bool UpdateAppointments(UpdateAppointment data)
        {
            DateTime convertedDate = data.Appointment.Date.Date;
            DateTime OldDate = (data.OldDate).Date;
            AddNewAppointment dataToBeUpdated = new AddNewAppointment()
            {
                Date = data.Appointment.Date,
                Title = data.Appointment.Title,
                Description = data.Appointment.Description,
                Type = data.Appointment.Type,
                StartTime = data.Appointment.StartTime,
                EndTime = data.Appointment.EndTime,
            };
            CheckInputTime(dataToBeUpdated);

            if (_appointmentsDAL.CheckForId(data.Appointment.Id, OldDate))
            {
                if (convertedDate == OldDate)
                {
                    return (_appointmentsDAL.UpdateAppointmentById(data.Appointment));
                }
                else
                {

                    var updatedAppointment = _appointmentsDAL.CreateNewAppointments(dataToBeUpdated);
                    if (updatedAppointment)
                    {
                        return _appointmentsDAL.RemoveAppointmentsById(data.Appointment.Id, OldDate);

                    }
                    else
                    {
                        return updatedAppointment;
                    }
                }
            }
            else
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.idIsInvalid));
            }
        }
        private void CheckInputTime(AddNewAppointment data)
        {
            if (data.StartTime == data.EndTime)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.startAndEndTimeAreSame));
            }
            if (data.EndTime < data.StartTime)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.startTimeGreaterThanEndTime));
            }
            int compare = DateTime.Compare(data.StartTime, DateTime.Now);
            if (compare == -1)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.tryingToAddMeetingInPastDate));
            }
        }
    }
}