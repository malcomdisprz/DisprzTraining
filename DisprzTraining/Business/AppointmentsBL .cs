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
            if (data.StartTime == data.EndTime)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.startAndEndTimeAreSame));
            }
            if (data.EndTime < data.StartTime)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.startTimeGreaterThanEndTime));
            }
            int comparedValue = DateTime.Compare(data.StartTime, DateTime.Now);
            if (comparedValue == -1)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.tryingToAddMeetingInPastDate));
            }
            try
            {
                return (_appointmentsDAL.CreateNewAppointments(data));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public Dictionary<DateTime, List<Appointment>> GetAllAddedAppointments()
        {
            return _appointmentsDAL.GetAllAppointments();
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
        public bool UpdateAppointments(Appointment data)
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
            try
            {
                return (_appointmentsDAL.UpdateAppointmentById(data));
            }
            catch(InvalidOperationException e)
            {
                throw new Exception(JsonConvert.SerializeObject(CustomErrorCodeMessages.idIsInvalid));  
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }





    }
}