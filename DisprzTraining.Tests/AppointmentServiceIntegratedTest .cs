using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
// using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;

namespace DisprzTraining.Tests
{
    public class AppointmentServiceIntegratedTest:IClassFixture<WebApplicationFactory<Program>> 
    {
        private readonly WebApplicationFactory<Program> _httpclient;

        public AppointmentServiceIntegratedTest(WebApplicationFactory<Program> httpclient)
        {
            _httpclient = httpclient;
        }
        [Fact]

        public async Task Get_All_Added_Appointments_Returns_Success_Code()
        {
         //Arrange
          var client=_httpclient.CreateClient();
          
        //Act
         var response=await client.GetAsync("api/appointments");

          response.EnsureSuccessStatusCode();

          var content=await response.Content.ReadAsStringAsync();

          var appointment=JsonConvert.DeserializeObject<List<Appointment>>(content);

        //Assert
          Assert.True(appointment?.Count>0);
          Assert.Equal(HttpStatusCode.OK,response.StatusCode);
          
        }
         [Fact]

        public async Task Get_Appointments_For_Day_Returns_Success_Code()
        {
         //Arrange
          var client=_httpclient.CreateClient();
         

        //Act
         var response=await client.GetAsync("api/appointments/12-21-2022");
          response.EnsureSuccessStatusCode();

          var content=await response.Content.ReadAsStringAsync();

          var appointment=JsonConvert.DeserializeObject<List<Appointment>>(content);
          
        //Assert
          Assert.True(appointment?.Count>0);
          Assert.Equal(HttpStatusCode.OK,response.StatusCode);
          

        }


         [Fact]

        public async Task Create_Appointment_Returns_Success_Code()
        {
         //Arrange
          var client=_httpclient.CreateClient();
           AddNewAppointment data = new AddNewAppointment()
            {
                Date = "12-19-2022",
                Title = "test",
                Description = "test-case",
                StartTime = DateTime.Now.AddMinutes(30),
                EndTime = DateTime.Now.AddHours(1),
            };
         
        //Act
          var response=await client.PostAsync("api/appointments",TestHelper.GetStringContent(data));
          response.EnsureSuccessStatusCode();

          var content=await response.Content.ReadAsStringAsync();

          var appointment=JsonConvert.DeserializeObject<bool>(content);
         
        //Assert
          Assert.True(appointment);
          Assert.Equal(HttpStatusCode.Created,response.StatusCode);

        }
        [Fact]
        public async Task Create_Appointment_Returns_Conflict_Code()
        {
         //Arrange
          var client=_httpclient.CreateClient();
           AddNewAppointment data = new AddNewAppointment()
            {
                Date = "12-21-2022",
                Title = "test",
                Description = "test-case",
                StartTime=new DateTime(2022,12,21,11,15,0),
              EndTime=new DateTime(2022,12,21,12,30,0),
            };
         
        //Act
          var response=await client.PostAsync("api/appointments",TestHelper.GetStringContent(data));

          
          Assert.Equal(HttpStatusCode.Conflict,response.StatusCode);

        }

        

        [Fact]
         public async Task Update_Returns_Success_Code()
        {
         //Arrange
          var client=_httpclient.CreateClient();
          Appointment data=new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
              Date="12-21-2022",
              Title="test-update",
              Description="test-scenarios-updated",
              StartTime=new DateTime(2022,12,21,11,0,0),
              EndTime=new DateTime(2022,12,21,12,0,0),
              };
          
      
        //Act
          var response=await client.PutAsync("api/appointments",TestHelper.GetStringContent(data));

          response.EnsureSuccessStatusCode();

          var content=await response.Content.ReadAsStringAsync();

          var appointment=JsonConvert.DeserializeObject<bool>(content);

        //Assert
          Assert.True(appointment);
          Assert.Equal(HttpStatusCode.OK,response.StatusCode);

        }
         [Fact]
         public async Task Update_Returns_Conflict()
        {
         //Arrange
          var client=_httpclient.CreateClient();
          Appointment data=new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
              Date="12-19-2022",
              Title="update-scenario",
              Description="test-update-url",
              StartTime=new DateTime(2022,12,21,11,0,0),
              EndTime=new DateTime(2022,12,21,12,0,0),
              };
          
      
        //Act
          var response=await client.PutAsync("api/appointments",TestHelper.GetStringContent(data));

        //Assert
           Assert.Equal(HttpStatusCode.Conflict,response.StatusCode);

        }
         [Fact]

        public async Task Remove_Returns_Success_Code()
        {
         //Arrange
          var client=_httpclient.CreateClient();
          
        //Act
        var response=await client.DeleteAsync("api/appointments/7c9e6679-7425-40de-944b-e07fc1f90ae7");

          response.EnsureSuccessStatusCode();

          var content=await response.Content.ReadAsStringAsync();

          var appointment=JsonConvert.DeserializeObject<bool>(content);

        //Assert
          Assert.True(appointment);
          Assert.Equal(HttpStatusCode.OK,response.StatusCode);

        }
        [Fact]
         public async Task Remove_Returns_NotFound_Code()
        {
         //Arrange
          var client=_httpclient.CreateClient();
          
        //Act
        var response=await client.DeleteAsync("api/appointments/7c9e6679-7425-40de-944b-e07fc1f90a7e");
          Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

        }
    }
}