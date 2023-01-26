using DisprzTraining.Business;
using DisprzTraining.Controllers;
using DisprzTraining.DataAccess;
using Microsoft.AspNetCore.Mvc;
using DisprzTraining.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DisprzTraining.Tests
{
    public class AppointmentDALTests
    {
        private readonly IAppointmentDAL appointmenDAL;
        public AppointmentDALTests()
        {
            appointmenDAL = new AppointmentDAL();
        }
        List<Appointment> items = new List<Appointment>()
         {
            new Appointment(){Id = Guid.NewGuid(), EventName = "Stand Up" , FromTime = new DateTime(2023,01,12,12,00,00) , ToTime = new DateTime(2023,01,12,13,00,00)}
         };
        Appointment obj = new Appointment { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Meeting", FromTime = new DateTime(2022, 12, 28, 16, 00, 00), ToTime = new DateTime(2022, 12, 28, 17, 00, 00) };
        // Appointment objs = new Appointment { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7ab"), EventName = "Meeting", FromTime = new DateTime(2022, 12, 28, 19, 00, 00), ToTime = new DateTime(2022, 12, 28, 20, 00, 00) };

        Appointment obj1 = new Appointment { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Stand Up", FromTime = new DateTime(2022, 12, 21, 12, 00, 00), ToTime = new DateTime(2022, 12, 21, 14, 00, 00) };
        Appointment ConflictObj = new Appointment() { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Meeting with EM", FromTime = new DateTime(2022, 12, 14, 16, 00, 00), ToTime = new DateTime(2022, 12, 14, 18, 00, 00) };
        Appointment obj2 = new Appointment { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Huddle", FromTime = new DateTime(2022, 12, 21, 12, 00, 00), ToTime = new DateTime(2022, 12, 21, 14, 00, 00) };
        Appointment ExistingObj = new Appointment() { Id = Guid.NewGuid(), EventName = "Meeting with EM", FromTime = new DateTime(2022, 12, 15, 17, 00, 00), ToTime = new DateTime(2022, 12, 15, 18, 00, 00) };
        Appointment ConflictObj1 = new Appointment() { Id = Guid.NewGuid(), EventName = "Meeting with EM", FromTime = new DateTime(2022, 12, 15, 16, 00, 00), ToTime = new DateTime(2022, 12, 15, 19, 00, 00) };


        [Fact]
        public async Task GetAllAppointments_WithExistingItem_ReturnsType()
        {
            // Act
            var result = await appointmenDAL.GetAllAppointmentsDALAsync();
            // Assert
            Assert.IsType<List<Appointment>>(result);
        }

        [Fact]
        public async Task GetAppointmentByDate_WithExistingItem_ReturnsType()
        {
            // Act
            var result = await appointmenDAL.GetAppointmentByDateDALAsync(new DateTime(2022, 12, 14, 13, 30, 00));
            // Assert
            Assert.IsType<List<Appointment>>(result);
        }

        [Fact]
        public async Task GetAppointmentByDate_WithUnexistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmenDAL.GetAppointmentByDateDALAsync(items[0].FromTime);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithexistingItem_ReturnsType()
        {
            // Act
            var result = await appointmenDAL.GetAppointmentByEventDALAsync("Daily Scrum");
            // Assert
            Assert.IsType<Appointment>(result);
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithUnexistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmenDAL.GetAppointmentByEventDALAsync(items[0].EventName);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAppointmentById_WithUnexistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmenDAL.GetAppointmentByIdDALAsync(Guid.NewGuid());
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetAppointmentById_WithExistingItem_ReturnsType()
        {
            // Act
            await appointmenDAL.CreateAppointmentDALAsync(obj);
            var result = await appointmenDAL.GetAppointmentByIdDALAsync(obj.Id);
            appointmenDAL.DeleteAppointmentDALAsync(obj.Id);

            // Assert
            Assert.IsType<Appointment>(result);
        }

        [Fact]
        public async Task CreateAppointment_AddingExistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmenDAL.CreateAppointmentDALAsync(ExistingObj);
            var conflictresult = await appointmenDAL.CreateAppointmentDALAsync(ConflictObj1);
            // Assert
            Assert.Null(result);
            Assert.Null(conflictresult);
        }

        [Fact]
        public async Task CreateAppointment_AddingUnexistingItem_ReturnsType()
        {
            // Act
            var result = await appointmenDAL.CreateAppointmentDALAsync(obj);
            // Assert
            Assert.IsType<Appointment>(result);
        }

        [Fact]
        public async Task UpdateAppointment_UpdateUnexistingItem_ReturnsNull()
        {
            // Act
            await appointmenDAL.CreateAppointmentDALAsync(obj);
            //  await appointmenDAL.CreateAppointmentDALAsync(objs);
            var result = await appointmenDAL.UpdateAppointmentDALAsync(new Appointment());
            var conflictresult = await appointmenDAL.UpdateAppointmentDALAsync(ConflictObj);
            appointmenDAL.DeleteAppointmentDALAsync(obj.Id);
            // Assert
            Assert.Null(result);
            Assert.Null(conflictresult);
        }

        [Fact]
        public async Task UpdateAppointment_UpdateExistingItem_ReturnsNull()
        {
            // Act
            await appointmenDAL.CreateAppointmentDALAsync(obj);
            var result = await appointmenDAL.UpdateAppointmentDALAsync(obj1);
            var sameId = await appointmenDAL.UpdateAppointmentDALAsync(obj2);
            appointmenDAL.DeleteAppointmentDALAsync(obj.Id);
            // Assert
            Assert.IsType<Appointment>(result);
            Assert.IsType<Appointment>(sameId);
        }

        [Fact]
        public async Task DeleteAppointment_DeletingUnexistingItem_ReturnsNull()
        {
            // Act
            var result = appointmenDAL.DeleteAppointmentDALAsync(Guid.NewGuid());
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteAppointment_DeletingExistingItem_ReturnsType()
        {
            // Act
            var create = await appointmenDAL.CreateAppointmentDALAsync(obj);
            var result = appointmenDAL.DeleteAppointmentDALAsync(obj.Id);
            // Assert
            result.Should().Be(Task.CompletedTask);
        }
    }
}
