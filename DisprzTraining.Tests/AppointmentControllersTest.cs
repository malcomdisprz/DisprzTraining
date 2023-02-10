using DisprzTraining.Business;
using DisprzTraining.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using DisprzTraining.Models;
using FluentAssertions;
using System.Net;

namespace DisprzTraining.Tests
{
    public class AppointmentControllerTest
    {
        AppointmentController controller;
        public AppointmentControllerTest()
        {
            controller = new AppointmentController(mock.Object);
        }
        private readonly Mock<IAppointmentBL> mock = new();
        List<Appointment> items = new List<Appointment>()
        {
            new Appointment(){Id = Guid.NewGuid(), EventName = "Stand Up" , FromTime = new DateTime(2023,01,01,13,30,00), ToTime = new DateTime(2023,01,01,15,30,00)}
        };
        Appointment obj = new Appointment() { Id = Guid.NewGuid(), EventName = "Stand Up", FromTime = new DateTime(2023, 01, 01, 13, 30, 00), ToTime = new DateTime(2023, 01, 01, 15, 30, 00) };

        //get all appointment

        [Fact]
        public async Task GetAllAppointments_WithExistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            mock.Setup(repo => repo.GetAllAppointmentsBLAsync()).ReturnsAsync(new List<Appointment>());
            // Act
            var result = (OkObjectResult)await controller.GetAllAppointmentsAsync();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task GetAllAppointments_WithUnexistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            mock.Setup(repo => repo.GetAllAppointmentsBLAsync()).ReturnsAsync(new List<Appointment>());
            // Act
            var result = (OkObjectResult)await controller.GetAllAppointmentsAsync();
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }

        //get all appointment by date

        [Fact]
        public async Task GetAppointmentByDate_WithExistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            mock.Setup(repo => repo.GetAppointmentByDateBLAsync(items[0].FromTime)).ReturnsAsync(items);
            // Act
            var result = (OkObjectResult)await controller.GetAppointmentByDateAsync(items[0].FromTime);
            List<Appointment> ResultValue = (List<Appointment>)result.Value;
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            ResultValue.Should().BeEquivalentTo(items);
            ResultValue.Should().OnlyContain
            (
                value => value.FromTime == items[0].FromTime
            );
        }

        [Fact]
        public async Task GetAppointmentByDate_WithUnexistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            mock.Setup(repo => repo.GetAppointmentByDateBLAsync(It.IsAny<DateTime>())).ReturnsAsync(() => null);
            // Act
            var result = (OkObjectResult)await controller.GetAppointmentByDateAsync(DateTime.Now);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }

        //get all appointment by event

        [Fact]
        public async Task GetAppointmentByEvent_WithexistingItem_ReturnsOkObjectResult()
        {
            //Arrange
            mock.Setup(repo => repo.GetAppointmentByEventBLAsync(obj.EventName)).ReturnsAsync(obj);
            // Act
            var result = (OkObjectResult)await controller.GetAppointmentByEventAsync(obj.EventName);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            mock.Setup(repo => repo.GetAppointmentByEventBLAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            // Act
            var result = (NotFoundObjectResult)await controller.GetAppointmentByEventAsync("Daily Huddle");
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }

        //get all appointment by id

        [Fact]
        public async Task GetAppointmentById_WithexistingItem_ReturnsOkObjectResult()
        {
            // Arrange
            mock.Setup(repo => repo.GetAppointmentByIdBLAsync(obj.Id)).ReturnsAsync(obj);
            // Act
            var result = (OkObjectResult)await controller.GetAppointmentByIdAsync(obj.Id);
            // Assert
            result.Should().BeOfType<OkObjectResult>();
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
        }


        [Fact]
        public async Task GetAppointmentById_WithUnexistingItem_ReturnsNotFound()
        {
            // Arrange
            mock.Setup(repo => repo.GetAppointmentByIdBLAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
            // Act
            var result = (NotFoundObjectResult)await controller.GetAppointmentByIdAsync(Guid.NewGuid());
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }
        //create appoinytment 

        [Fact]
        public async Task CreateAppointment_AddingInvalidTimeInterval_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var item = new Appointment() { Id = Guid.NewGuid(), EventName = "Stand Up", FromTime = new DateTime(2022, 12, 14, 13, 30, 00), ToTime = new DateTime(2022, 12, 14, 12, 00, 00) };
            mock.Setup(repo => repo.CreateAppointmentBLAsync(It.IsAny<Appointment>())).ReturnsAsync(item);
            //Act
            var result = (BadRequestObjectResult)await controller.CreateAppointmentAsync(item);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_AddingExistingItem_ReturnsConflictObjectResult()
        {
            // Arrange
            mock.Setup(repo => repo.CreateAppointmentBLAsync(It.IsAny<Appointment>())).ReturnsAsync(() => null);
            //Act
            var result = (ConflictObjectResult)await controller.CreateAppointmentAsync(obj);
            //Assert
            result.Should().BeOfType<ConflictObjectResult>();
            Assert.Equal(HttpStatusCode.Conflict, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task CreateAppointment_AddingUnexistingItem_ReturnsCreatedResult()
        {
            // Arrange
            var obj = new Appointment() { Id = Guid.NewGuid(), EventName = "Daily Scrum", FromTime = new DateTime(2022, 12, 14, 13, 30, 00), ToTime = new DateTime(2022, 12, 14, 15, 00, 00) };
            mock.Setup(repo => repo.CreateAppointmentBLAsync(It.IsAny<Appointment>())).ReturnsAsync(obj);
            //Act
            var result = (CreatedResult)await controller.CreateAppointmentAsync(obj);
            //Assert
            result.Should().BeOfType<CreatedResult>();
            Assert.Equal(HttpStatusCode.Created, (HttpStatusCode)result.StatusCode);
        }

        //update appointment
        [Fact]
        public async Task UpdateAppointment_AddingInvalidTimeInterval_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var item = new Appointment() { Id = Guid.NewGuid(), EventName = "Stand Up", FromTime = new DateTime(2022, 12, 14, 13, 30, 00), ToTime = new DateTime(2022, 12, 14, 12, 00, 00) };
            mock.Setup(repo => repo.UpdateAppointmentBLAsync(It.IsAny<Appointment>())).ReturnsAsync(item);
            //Act
            var result = (BadRequestObjectResult)await controller.UpdateAppointmentAsync(item);
            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task UpdateAppointment_UpdateUnexistingItem_ReturnsConflictObjectResult()
        {
            // Arrange
            mock.Setup(repo => repo.CreateAppointmentBLAsync(It.IsAny<Appointment>())).ReturnsAsync(() => null);
            //Act
            var result = (ConflictObjectResult)await controller.UpdateAppointmentAsync(obj);
            //Assert
            result.Should().BeOfType<ConflictObjectResult>();
            Assert.Equal(HttpStatusCode.Conflict, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task UpdateAppointment_UpdateExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            mock.Setup(repo => repo.UpdateAppointmentBLAsync(It.IsAny<Appointment>())).ReturnsAsync(obj);
            //Act
            var result = (NoContentResult)await controller.UpdateAppointmentAsync(obj);
            //Assert
            result.Should().BeOfType<NoContentResult>();
            Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)result.StatusCode);
        }


        [Fact]
        public async Task DeleteAppointment_DeletingExistingItem_ReturnsNoContentResult()
        {
            // Arrange
            mock.Setup(repo => repo.DeleteAppointmentBLAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);
            //Act
            var result = (NoContentResult)await controller.DeleteAppointmentAsync(Guid.NewGuid());
            //Assert
            result.Should().BeOfType<NoContentResult>();
            Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)result.StatusCode);
        }

        [Fact]
        public async Task DeleteAppointment_DeletingUnexistingItem_ReturnsNoContentResult()
        {
            // Arrange
            mock.Setup(repo => repo.DeleteAppointmentBLAsync(It.IsAny<Guid>())).Returns(() => null);
            //Act
            var result = (NotFoundObjectResult)await controller.DeleteAppointmentAsync(Guid.NewGuid());
            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)result.StatusCode);
        }
    }
}