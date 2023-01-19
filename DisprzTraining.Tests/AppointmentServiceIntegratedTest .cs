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
using System.Net.Http.Json;

namespace DisprzTraining.Tests
{
    public class AppointmentServiceIntegratedTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpclient;

        public AppointmentServiceIntegratedTest()
        {
            var sut = new WebApplicationFactory<Program>();
            _httpclient = sut.CreateClient();
        }
        public string URL = "api/appointments";
        // [Fact]

        // public async Task Get_All_Added_Appointments_Returns_Success_Code()
        // {
        //     //Arrange
        //     // var client = _httpclient.CreateClient();


        //     //Act
        //     var response = await _httpclient.GetAsync(URL);

        //     response.EnsureSuccessStatusCode();

        //     var content = await response.Content.ReadAsStringAsync();

        //     var appointment = JsonConvert.DeserializeObject<List<Appointment>>(content);

        //     //Assert
        //     Assert.True(appointment?.Count > 0);
        //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // }
        //     [Fact]

        //     public async Task Get_Appointments_For_Day_Returns_Success_Code()
        //     {
        //         DateTime date = new DateTime(2022, 12, 18, 0, 0, 0);
        //         //Arrange
        //         var client = _httpclient.CreateClient();


        //         //Act
        //         var response = await client.GetAsync($"api/appointments/{date}");
        //         response.EnsureSuccessStatusCode();

        //         var content = await response.Content.ReadAsStringAsync();

        //         var appointment = JsonConvert.DeserializeObject<List<Appointment>>(content);

        //         //Assert
        //         Assert.True(appointment?.Count > 0);
        //         Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        //     }


        [Fact]

        public async Task Create_Appointment_Returns_Success_Code()
        {
            //Arrange
            // var client = _httpclient.CreateClient();
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 31),
                Title = "test",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 0, 0),
            };
            //Act
            var response = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var appointment = JsonConvert.DeserializeObject<bool>(content);

            var response_2 = await _httpclient.GetAsync($"{URL}/2023-01-31T00:00:00");

            response_2.EnsureSuccessStatusCode();

            var content_2 = response_2.Content.ReadFromJsonAsync<List<Appointment>>();

            var getId = content_2.Result[0].Id;


            //Assert
            Assert.True(content_2.Result.Count > 0);
            Assert.Equal(HttpStatusCode.OK, response_2.StatusCode);
            Assert.True(appointment);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var deleteResponse = await _httpclient.DeleteAsync($"{URL}/{getId}/2023-01-31T00:00:00");
        }

        [Fact]

        public async Task Create_Appointment_Returns_Conflict()
        {
            //Arrange
            // var client = _httpclient.CreateClient();
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 31),
                Title = "test",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 0, 0),
            };
            //time conflicts
            AddNewAppointment data_2 = new AddNewAppointment()
            {
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 0, 0),
            };
            //Act
            var response = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var appointment = JsonConvert.DeserializeObject<bool>(content);

            var postResponse = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_2));

            var response_2 = await _httpclient.GetAsync($"{URL}/2023-01-31T00:00:00");

            var content_2 = response_2.Content.ReadFromJsonAsync<List<Appointment>>();

            var getId = content_2.Result[0].Id;

            var deleteResponse = await _httpclient.DeleteAsync($"{URL}/{getId}/2023-01-31T00:00:00");
            //Assert
            Assert.Equal(HttpStatusCode.Conflict, postResponse.StatusCode);
        }

        [Fact]

        public async Task Create_Appointment_Bad_Request()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 31),
                Title = "test",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 0, 0),
            };
            //start-time and end-time same
            AddNewAppointment data_2 = new AddNewAppointment()
            {
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 7, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 7, 0, 0),
            };

            //start time greater than end time
            AddNewAppointment data_3 = new AddNewAppointment()
            {
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 7, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 6, 0, 0),
            };
            //Act
            var response = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var appointment = JsonConvert.DeserializeObject<bool>(content);

            var postResponse = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_2));

            var postResponse_2 = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_3));

            var response_2 = await _httpclient.GetAsync($"{URL}/2023-01-31T00:00:00");

            var content_2 = response_2.Content.ReadFromJsonAsync<List<Appointment>>();

            var getId = content_2.Result[0].Id;

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, postResponse.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, postResponse_2.StatusCode);

            var deleteResponse = await _httpclient.DeleteAsync($"{URL}/{getId}/2023-01-31T00:00:00");

        }

        [Fact]
        public async Task Update_Appointment_Returns_Ok_Result()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 31),
                Title = "test",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 0, 0),
            };

            AddNewAppointment data_2 = new AddNewAppointment()
            {
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 5, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 6, 0, 0),
            };

            //Act
            var response = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var appointment = JsonConvert.DeserializeObject<bool>(content);

            var postResponse = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_2));

            // var postResponse_2= await _httpclient.PostAsync(URL,TestHelper.GetStringContent(data_3));


            var response_2 = await _httpclient.GetAsync($"{URL}/2023-01-31T00:00:00");

            var content_2 = response_2.Content.ReadFromJsonAsync<List<Appointment>>();

            var getId = content_2.Result[0].Id;
            Appointment updateData = new Appointment()
            {
                Id = content_2.Result[1].Id,
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 8, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 9, 0, 0),
            };
            var updateResponse = await _httpclient.PutAsync(URL, TestHelper.GetStringContent(updateData));

            //Assert
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[1].Id}/2023-01-31T00:00:00");
            var deleteResponse = await _httpclient.DeleteAsync($"{URL}/{getId}/2023-01-31T00:00:00");

        }


        [Fact]
        public async Task Update_Appointment_Returns_Conflict()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 31),
                Title = "test",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 0, 0),
            };

            AddNewAppointment data_2 = new AddNewAppointment()
            {
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 5, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 6, 0, 0),
            };

            //Act
            var response = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var appointment = JsonConvert.DeserializeObject<bool>(content);

            var postResponse = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_2));

            // var postResponse_2= await _httpclient.PostAsync(URL,TestHelper.GetStringContent(data_3));


            var response_2 = await _httpclient.GetAsync($"{URL}/2023-01-31T00:00:00");

            var content_2 = response_2.Content.ReadFromJsonAsync<List<Appointment>>();

            var getId = content_2.Result[0].Id;
            Appointment updateData = new Appointment()
            {
                Id = content_2.Result[1].Id,
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 30, 0),
            };
            var updateResponse = await _httpclient.PutAsync(URL, TestHelper.GetStringContent(updateData));

            //Assert
            Assert.Equal(HttpStatusCode.Conflict, updateResponse.StatusCode);
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[1].Id}/2023-01-31T00:00:00");
            var deleteResponse = await _httpclient.DeleteAsync($"{URL}/{getId}/2023-01-31T00:00:00");

        }



        [Fact]
        public async Task Update_Appointment_Returns_Bad_Request()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 31),
                Title = "test",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 2, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 3, 0, 0),
            };

            AddNewAppointment data_2 = new AddNewAppointment()
            {
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 5, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 6, 0, 0),
            };

            //Act
            var response = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var appointment = JsonConvert.DeserializeObject<bool>(content);

            var postResponse = await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_2));

            // var postResponse_2= await _httpclient.PostAsync(URL,TestHelper.GetStringContent(data_3));


            var response_2 = await _httpclient.GetAsync($"{URL}/2023-01-31T00:00:00");

            var content_2 = response_2.Content.ReadFromJsonAsync<List<Appointment>>();

            var getId = content_2.Result[0].Id;
            Appointment updateData = new Appointment()
            {
                Id = content_2.Result[1].Id,
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 7, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 7, 0, 0),
            };

            Appointment updateData_2 = new Appointment()
            {
                Id = content_2.Result[1].Id,
                Date = data.Date,
                Title = "test-2",
                Description = "test-case",
                Type = "Reminder",
                StartTime = new DateTime(2023, 01, 31, 7, 0, 0),
                EndTime = new DateTime(2023, 01, 31, 6, 30, 0),
            };
            var updateResponse = await _httpclient.PutAsync(URL, TestHelper.GetStringContent(updateData));
            var updateResponse_2 = await _httpclient.PutAsync(URL, TestHelper.GetStringContent(updateData_2));
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, updateResponse.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, updateResponse_2.StatusCode);
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[1].Id}/2023-01-31T00:00:00");
            var deleteResponse = await _httpclient.DeleteAsync($"{URL}/{getId}/2023-01-31T00:00:00");

        }


        [Fact]
        public async Task Get_List_By_Range()
        {
            //Arrange
            AddNewAppointment data_1 = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 26),
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime = new DateTime(2023, 01, 26, 11, 0, 0),
                EndTime = new DateTime(2023, 01, 26, 12, 0, 0),
            };
            AddNewAppointment data_2 = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 27),
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime = new DateTime(2023, 01, 27, 11, 0, 0),
                EndTime = new DateTime(2023, 01, 27, 12, 0, 0)
            };
            AddNewAppointment data_3 = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 27),
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime = new DateTime(2023, 01, 27, 13, 0, 0),
                EndTime = new DateTime(2023, 01, 27, 14, 0, 0)
            };
            AddNewAppointment data_4 = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 28),
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime = new DateTime(2023, 01, 28, 13, 0, 0),
                EndTime = new DateTime(2023, 01, 28, 14, 0, 0)
            };
            AddNewAppointment data_5 = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 29),
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime = new DateTime(2023, 01, 29, 13, 0, 0),
                EndTime = new DateTime(2023, 01, 29, 14, 0, 0)
            };
            AddNewAppointment data_6 = new AddNewAppointment()
            {
                Date = new DateTime(2023, 01, 30),
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime = new DateTime(2023, 01, 30, 13, 0, 0),
                EndTime = new DateTime(2023, 01, 30, 14, 0, 0)
            };

            // DateTime date = new DateTime(2023, 01, 26);

            //Act
            await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_1));
            await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_2));
            await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_3));
            await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_4));
            await _httpclient.PostAsync(URL, TestHelper.GetStringContent(data_5));

            var response_2 = await _httpclient.GetAsync($"{URL}/range/2023-01-26T00:00:00");
            var content_2 = response_2.Content.ReadFromJsonAsync<List<Appointment>>();

            //Assert
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[0].Id}/2023-01-31T00:00:00");
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[0].Id}/2023-01-31T00:00:00");
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[0].Id}/2023-01-31T00:00:00");
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[0].Id}/2023-01-31T00:00:00");
            await _httpclient.DeleteAsync($"{URL}/{content_2.Result[0].Id}/2023-01-31T00:00:00");

        }






        //     [Fact]
        //     public async Task Create_Appointment_Returns_Conflict_Code()
        //     {
        //         //Arrange
        //         var client = _httpclient.CreateClient();
        //         AddNewAppointment data = new AddNewAppointment()
        //         {
        //             Date = new DateTime(2022, 12, 18, 0, 0, 0),
        //             Title = "test",
        //             Description = "test-case",
        //             Type = "reminder",
        //             StartTime = new DateTime(2022, 12, 21, 11, 15, 0),
        //             EndTime = new DateTime(2022, 12, 21, 12, 30, 0),
        //         };

        //         //Act
        //         var response = await client.PostAsync("api/appointments", TestHelper.GetStringContent(data));


        //         Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        //     }


        //     [Fact]
        //     public async Task Create_Appointment_Throws_Exception_And_Conflict_Code()
        //     {
        //         //Arrange
        //         var client = _httpclient.CreateClient();
        //         AddNewAppointment data = new AddNewAppointment()
        //         {
        //             Date = new DateTime(2022, 12, 18, 0, 0, 0),
        //             Title = "test",
        //             Description = "test-case",
        //             Type = "reminder",
        //             StartTime = new DateTime(2022, 12, 21, 11, 15, 0),
        //             EndTime = new DateTime(2022, 12, 21, 11, 15, 0),
        //         };

        //         //Act
        //         var response = await client.PostAsync("api/appointments", TestHelper.GetStringContent(data));
        //         //Assert
        //         Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        //     }
        //     [Fact]
        //     public async Task Update_Returns_Success_Code()
        //     {
        //         //Arrange
        //         var client = _httpclient.CreateClient();
        //         Appointment data = new Appointment()
        //         {
        //             Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
        //             Date = new DateTime(2022, 12, 18, 0, 0, 0),
        //             Title = "test-update",
        //             Description = "test-scenarios-updated",
        //             Type = "reminder",
        //             StartTime = new DateTime(2022, 12, 21, 11, 0, 0),
        //             EndTime = new DateTime(2022, 12, 21, 12, 0, 0),
        //         };


        //         //Act
        //         var response = await client.PutAsync("api/appointments", TestHelper.GetStringContent(data));

        //         response.EnsureSuccessStatusCode();

        //         var content = await response.Content.ReadAsStringAsync();

        //         var appointment = JsonConvert.DeserializeObject<bool>(content);

        //         //Assert
        //         Assert.True(appointment);
        //         Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //     }
        //     [Fact]
        //     public async Task Update_Returns_Conflict()
        //     {
        //         //Arrange
        //         var client = _httpclient.CreateClient();
        //         Appointment data = new Appointment()
        //         {
        //             Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
        //             Date = new DateTime(2022, 12, 18, 0, 0, 0),
        //             Title = "update-scenario",
        //             Description = "test-update-url",
        //             Type = "reminder",
        //             StartTime = new DateTime(2022, 12, 21, 11, 0, 0),
        //             EndTime = new DateTime(2022, 12, 21, 12, 0, 0),
        //         };


        //         //Act
        //         var response = await client.PutAsync("api/appointments", TestHelper.GetStringContent(data));

        //         //Assert
        //         Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

        //     }
        //     [Fact]

        //     public async Task Remove_Returns_Success_Code()
        //     {
        //         //Arrange
        //         var client = _httpclient.CreateClient();

        //         //Act
        //         var response = await client.DeleteAsync("api/appointments/7c9e6679-7425-40de-944b-e07fc1f90ae7");

        //         response.EnsureSuccessStatusCode();

        //         var content = await response.Content.ReadAsStringAsync();


        //         //Assert

        //         Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        //     }
        //     [Fact]
        //     public async Task Remove_Returns_NotFound_Code()
        //     {
        //         //Arrange
        //         var client = _httpclient.CreateClient();

        //         //Act
        //         var response = await client.DeleteAsync("api/appointments/7c9e6679-7425-40de-944b-e07fc1f90a7e");
        //         Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        //     }
    }
}