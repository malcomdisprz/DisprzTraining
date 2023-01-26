using DisprzTraining.Business;
using DisprzTraining.Controllers;
using DisprzTraining.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using DisprzTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;


namespace DisprzTraining.Tests
{
    public class AppointmentBLTests
    {
        AppointmentBL BussinessLayer;
        public AppointmentBLTests()
        {
            BussinessLayer = new AppointmentBL(mockDAL.Object);
        }
        private readonly Mock<IAppointmentDAL> mockDAL = new();
        List<Appointment> items = new List<Appointment>()
        {
            new Appointment(){Id = Guid.NewGuid(), EventName = "Stand Up" , FromTime = DateTime.Now , ToTime = DateTime.Now}
        };
        Appointment obj = new Appointment { Id = Guid.NewGuid(), EventName = "Stand Up", FromTime = DateTime.Now, ToTime = DateTime.Now };

        //get all appointment
        [Fact]
        public async Task GetAllAppointments_WithExistingItem_ReturnsType()
        {
            // Arrange
            mockDAL.Setup(repo => repo.GetAllAppointmentsDALAsync()).ReturnsAsync(new List<Appointment>());
            // Act
            var result = await BussinessLayer.GetAllAppointmentsBLAsync();
            // Assert
            result.Should().BeOfType<List<Appointment>>();
        }

        [Fact]
        public async Task GetAllAppointments_WithUnexistingItem_ReturnsNull()
        {
            // Arrange
            mockDAL.Setup(repo => repo.GetAllAppointmentsDALAsync()).ReturnsAsync(() => null);
            // Act
            var result = await BussinessLayer.GetAllAppointmentsBLAsync();
            // Assert
            result.Should().BeNull();
        }

        //get all appointment by date
        [Fact]
        public async Task GetAppointmentByDate_WithExistingItem_ReturnsType()
        {
            // Arrange
            mockDAL.Setup(repo => repo.GetAppointmentByDateDALAsync(items[0].FromTime)).ReturnsAsync(items);
            // Act
            var result = await BussinessLayer.GetAppointmentByDateBLAsync(items[0].FromTime);
            // Assert
            result.Should().BeOfType<List<Appointment>>();
            result.Should().BeEquivalentTo(items);
            result.Should().OnlyContain
            (
                value => value.FromTime == items[0].FromTime
            );
        }

         [Fact]
        public async Task GetAppointmentByDate_WithUnexistingItem_ReturnsNull()
        {
            // Arrange
            mockDAL.Setup(repo => repo.GetAppointmentByDateDALAsync(It.IsAny<DateTime>())).ReturnsAsync(() => null);
            // Act
            var result = await BussinessLayer.GetAppointmentByDateBLAsync(DateTime.Now);
            // Assert
            result.Should().BeNull();
        }

         //get all appointment by event
        [Fact]
        public async Task GetAppointmentByEvent_WithexistingItem_ReturnsType()
        {
            //Arrange
            mockDAL.Setup(repo => repo.GetAppointmentByEventDALAsync(obj.EventName)).ReturnsAsync(obj);
            // Act
            var result = await BussinessLayer.GetAppointmentByEventBLAsync(obj.EventName);
            // Assert
            result.Should().BeOfType<Appointment>();
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithUnexistingItem_ReturnsNull()
        {
            // Arrange
            mockDAL.Setup(repo => repo.GetAppointmentByEventDALAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            // Act
            var result = await BussinessLayer.GetAppointmentByEventBLAsync("Daily Huddle");
            // Assert
            result.Should().BeNull();
        }

        //get all appointment by id

        [Fact]
        public async Task GetAppointmentById_WithexistingItem_ReturnsType()
        {
            // Arrange
            mockDAL.Setup(repo => repo.GetAppointmentByIdDALAsync(obj.Id)).ReturnsAsync(obj);
            // Act
            var result = await BussinessLayer.GetAppointmentByIdBLAsync(obj.Id);
            // Assert
            result.Should().BeOfType<Appointment>();
        }

        [Fact]
        public async Task GetAppointmentById_WithUnexistingItem_ReturnsNull()
        {
            // Arrange
            mockDAL.Setup(repo => repo.GetAppointmentByIdDALAsync(It.IsAny<Guid>())).ReturnsAsync(() => null);
            // Act
            var result = await BussinessLayer.GetAppointmentByIdBLAsync(Guid.NewGuid());
            // Assert
            result.Should().BeNull();
        }

    //    create appointrment
         [Fact]
        public async Task CreateAppointment_AddingExistingItem_ReturnsNull()
        {
            // Arrange
            mockDAL.Setup(repo => repo.CreateAppointmentDALAsync(It.IsAny<Appointment>())).ReturnsAsync(() => null);
            //Act
            var result = BussinessLayer.CreateAppointmentBLAsync(new Appointment());
            //Assert
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAppointment_AddingUnexistingItem_ReturnsType()
        {
            // Arrange
            mockDAL.Setup(repo => repo.CreateAppointmentDALAsync(It.IsAny<Appointment>())).ReturnsAsync(obj);
            //Act
            var result = BussinessLayer.CreateAppointmentBLAsync(new Appointment());
            //Assert
            result.Result.Should().BeOfType<Appointment>();
        }

        //update appointment
        [Fact]
        public async Task UpdateAppointment_UpdateUnexistingItem_ReturnsConflictObjectResult()
        {
            // Arrange
            var x = mockDAL.Setup(repo => repo.UpdateAppointmentDALAsync(It.IsAny<Appointment>())).ReturnsAsync(() => null);
            //Act
            var CreateItem = await BussinessLayer.UpdateAppointmentBLAsync(new Appointment());
            //Assert
            CreateItem.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAppointment_UpdateExistingItem_ReturnsType()
        {
            // Arrange
            mockDAL.Setup(repo => repo.UpdateAppointmentDALAsync(It.IsAny<Appointment>())).ReturnsAsync(obj);
            //Act
            var CreateItem = await BussinessLayer.UpdateAppointmentBLAsync(new Appointment());
            //Assert
            CreateItem.Should().BeOfType<Appointment>();
        }

        [Fact]
        public async Task DeleteAppointment_DeletingUnexistingItem_ReturnsNull()
        {
             // Arrange
            mockDAL.Setup(repo => repo.DeleteAppointmentDALAsync(It.IsAny<Guid>())).Returns(()=>null);
            //Act
            var DeleteItem =  BussinessLayer.DeleteAppointmentBLAsync(Guid.NewGuid());
            //Assert
            DeleteItem.Should().BeNull();
        }
    }
}