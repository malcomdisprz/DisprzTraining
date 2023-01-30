using DisprzTraining.Business;
using DisprzTraining.Controllers;
using DisprzTraining.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using DisprzTraining.Models;
using System.Net;
using System.Net.Http.Json;

namespace DisprzTraining.Tests
{
    public class AppointmentIntegrationTesting
    {
        public static IAppointmentDAL appointmenDAL = new AppointmentDAL();
        public static IAppointmentBL appointmentBL = new AppointmentBL(appointmenDAL);
        AppointmentController appoinment = new(appointmentBL);
        private readonly HttpClient _client;
        public AppointmentIntegrationTesting()
        {
            var appFactory = new WebApplicationFactory<AppointmentController>();
            _client = appFactory.CreateClient();
        }

        [Fact]
        public async Task GetAllAppointments_WithExistingItem()
        {
            var response = await _client.GetAsync("http://localhost:5169/appointments");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task GetAppointmentByDate_WithExistingItem()
        {
            var response = await _client.GetAsync("http://localhost:5169/appointments/date?Date=2022-12-14");
            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithexistingItem()
        {
            var response = await _client.GetAsync("http://localhost:5169/appointments/event?Event=Meeting");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Meeting", responseString);
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithUnExistingItem()
        {
            var response = await _client.GetAsync("http://localhost:5169/appointments/event?Event=StandUp");
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Contains("No event found", responseString);

        }


        [Fact]
        public async Task CreateAppointment_AddingUnexistingItem()
        {
            var postRequest = new Appointment
            {
                Id = Guid.NewGuid(),
                EventName = "StandUp",
                FromTime = new DateTime(2023, 01, 28, 16, 00, 00),
                ToTime = new DateTime(2023, 01, 28, 17, 00, 00)
            };

            var response = await _client.PostAsJsonAsync("http://localhost:5169/appointments", postRequest);

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("StandUp", responseString);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_AddingExistingItem()
        {
            var postRequest = new Appointment
            {
                Id = Guid.NewGuid(),
                EventName = "Daily Scrum",
                FromTime = new DateTime(2022, 12, 14, 13, 30, 00),
                ToTime = new DateTime(2022, 12, 14, 15, 00, 00)
            };

            var response = await _client.PostAsJsonAsync("http://localhost:5169/appointments", postRequest);

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Meeting Exixts in the given time", responseString);
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_AddingInvalidTimeInterval()
        {
            var postRequest = new Appointment
            {
                Id = Guid.NewGuid(),
                EventName = "Daily Scrum",
                FromTime = new DateTime(2022, 12, 14, 13, 30, 00),
                ToTime = new DateTime(2022, 12, 14, 12, 00, 00)
            };

            var response = await _client.PostAsJsonAsync("http://localhost:5169/appointments", postRequest);

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Contains("Invalid time interval", responseString);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


    }
}