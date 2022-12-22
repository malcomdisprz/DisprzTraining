using DisprzTraining.Models;

namespace DisprzTraining.Tests.MockData
{
    public class MockSample
    {
         public static List<Appointment> data=new List<Appointment>()
          {
            new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
              Date="18-12-2022",
              Title="lk",
              Description="lkljkhgfds",
              StartTime=DateTime.Now,
              EndTime=DateTime.Now.AddHours(2),
              },
              new Appointment(){
              Id=new Guid("7c9e6679-7425-41de-944b-e70fc1f90ae7"),
              Date="18-12-2022",
              Title="event",
              Description="scenarios",
              StartTime=DateTime.Now,
              EndTime=DateTime.Now.AddHours(3),
              }
          };
    }
}