using DisprzTraining.Models;
namespace DisprzTraining.Data
{
    public static class DataStore
    {
        public static List<Appointment> newList = new List<Appointment>()
       {
         new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
              Date="12-21-2022",
              Title="lk",
              Description="test-scenarios",
              StartTime=new DateTime(2022,12,21,11,0,0),
              EndTime=new DateTime(2022,12,21,12,0,0),
              },
          new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f80ae7"),
              Date="12-18-2022",
              Title="lk",
              Description="test-scenarios",
              StartTime=new DateTime(2022,12,18,4,0,0),
              EndTime=new DateTime(2022,12,18,5,0,0),
              }
       };

    }
}