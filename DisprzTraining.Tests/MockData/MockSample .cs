using DisprzTraining.Models;

namespace DisprzTraining.Tests.MockData
{
    public class MockSample
    {
         public static List<Appointment> data=new List<Appointment>()
          {
            new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
              Date=new DateTime(2022,12,21,0,0,0),
              Title="lk",
              Description="test-scenarios",
              Type="reminder",
              StartTime=new DateTime(2022,12,21,11,0,0),
              EndTime=new DateTime(2022,12,21,12,0,0),
              },
              new Appointment(){
              Id=new Guid("7c9e6679-7425-41de-944b-e70fc1f90ae7"),
              Date=new DateTime(2022,12,21,0,0,0),
              Title="event",
              Type="reminder",
              Description="scenarios",
              StartTime=new DateTime(2022,12,21,13,0,0),
              EndTime=new DateTime(2022,12,21,14,0,0),
              }
          };
          
    }
}