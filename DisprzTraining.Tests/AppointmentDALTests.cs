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
            new Appointment(){Id = Guid.NewGuid(), EventName = "Stand Up" , FromTime = DateTime.Now , ToTime = DateTime.Now}     
         };
         Appointment obj = new Appointment { Id = Guid.NewGuid(), EventName = "Stand Up", FromTime = DateTime.Now, ToTime = DateTime.Now };
         Appointment ExistingObj = new Appointment(){Id=Guid.NewGuid(),EventName="Meeting with EM",FromTime=new DateTime(2022,12,14,17,00,00),ToTime= new DateTime(2022,12,14,18,00,00)};
        
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
            var result = await appointmenDAL.GetAppointmentByDateDALAsync(new DateTime(2022,12,14,13,30,00));
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
            Assert.IsType<List<Appointment>>(result);
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
         public async Task CreateAppointment_AddingExistingItem_ReturnsNull()
         {
            // Act
            var result = await appointmenDAL.CreateAppointmentDALAsync(ExistingObj);
            // Assert
            Assert.Null(result);
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
         public async Task UpdateAppointment_UpdateUnexistingItem_ReturnsType()
         {
            // Act
            var result = await appointmenDAL.CreateAppointmentDALAsync(obj);
            // Assert
            Assert.IsType<Appointment>(result);
         }

         [Fact]
         public async Task UpdateAppointment_UpdateExistingItem_ReturnsNull()
         {
            // Act
            var result = await appointmenDAL.CreateAppointmentDALAsync(ExistingObj);
            // Assert
            Assert.Null(result);
         }

         [Fact]
         public async Task DeleteAppointment_DeletingUnexistingItem_ReturnsNull()
         {
            // Act
            var result =  appointmenDAL.DeleteAppointmentDALAsync(Guid.NewGuid());
            // Assert
            Assert.Null(result);
         }
    }        
}
