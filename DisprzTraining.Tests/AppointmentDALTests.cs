using DisprzTraining.DataAccess;
using DisprzTraining.Models;
using FluentAssertions;

namespace DisprzTraining.Tests
{
    public class AppointmentDALTests
    {
        private readonly IAppointmentDAL appointmentDAL;
        public AppointmentDALTests()
        {
            appointmentDAL = new AppointmentDAL();
        }
        List<Appointment> items = new List<Appointment>()
         {
            new Appointment(){Id = Guid.NewGuid(), EventName = "Stand Up" , FromTime = new DateTime(2023,01,12,12,00,00) , ToTime = new DateTime(2023,01,12,13,00,00)}
         };
        Appointment obj = new Appointment { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Meeting", FromTime = new DateTime(2022, 12, 28, 16, 00, 00), ToTime = new DateTime(2022, 12, 28, 17, 00, 00) };
        Appointment obj1 = new Appointment { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Stand Up", FromTime = new DateTime(2022, 12, 21, 12, 00, 00), ToTime = new DateTime(2022, 12, 21, 14, 00, 00) };
        Appointment ConflictObj = new Appointment() { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Meeting with EM", FromTime = new DateTime(2022, 12, 14, 16, 00, 00), ToTime = new DateTime(2022, 12, 14, 18, 00, 00) };
        Appointment obj2 = new Appointment { Id = new Guid("c7b8e8c3-5ab5-4638-9469-16262b9ce7af"), EventName = "Huddle", FromTime = new DateTime(2022, 12, 21, 12, 00, 00), ToTime = new DateTime(2022, 12, 21, 14, 00, 00) };
        Appointment ExistingObj = new Appointment() { Id = Guid.NewGuid(), EventName = "Meeting with EM", FromTime = new DateTime(2022, 12, 15, 17, 00, 00), ToTime = new DateTime(2022, 12, 15, 18, 00, 00) };
        Appointment ConflictObj1 = new Appointment() { Id = Guid.NewGuid(), EventName = "Meeting with EM", FromTime = new DateTime(2022, 12, 15, 16, 00, 00), ToTime = new DateTime(2022, 12, 15, 19, 00, 00) };


        [Fact]
        public async Task GetAllAppointments_WithExistingItem_ReturnsType()
        {
            // Act
            var result = await appointmentDAL.GetAllAppointmentsDALAsync();
            // Assert
            Assert.IsType<List<Appointment>>(result);
        }

        [Fact]
        public async Task GetAppointmentByDate_WithExistingItem_ReturnsType()
        {
            // Act
            var result = await appointmentDAL.GetAppointmentByDateDALAsync(new DateTime(2022, 12, 14, 13, 30, 00));
            // Assert
            Assert.IsType<List<Appointment>>(result);
        }

        [Fact]
        public async Task GetAppointmentByDate_WithUnexistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmentDAL.GetAppointmentByDateDALAsync(items[0].FromTime);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithexistingItem_ReturnsType()
        {
            // Act
            var result = await appointmentDAL.GetAppointmentByEventDALAsync("Daily Scrum");
            // Assert
            Assert.IsType<Appointment>(result);
        }

        [Fact]
        public async Task GetAppointmentByEvent_WithUnexistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmentDAL.GetAppointmentByEventDALAsync(items[0].EventName);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAppointmentById_WithUnexistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmentDAL.GetAppointmentByIdDALAsync(Guid.NewGuid());
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetAppointmentById_WithExistingItem_ReturnsType()
        {
            // Act
            await appointmentDAL.CreateAppointmentDALAsync(obj);
            var result = await appointmentDAL.GetAppointmentByIdDALAsync(obj.Id);
            appointmentDAL.DeleteAppointmentDALAsync(obj.Id);

            // Assert
            Assert.IsType<Appointment>(result);
        }

        [Fact]
        public async Task CreateAppointment_AddingExistingItem_ReturnsNull()
        {
            // Act
            var result = await appointmentDAL.CreateAppointmentDALAsync(ExistingObj);
            var conflictresult = await appointmentDAL.CreateAppointmentDALAsync(ConflictObj1);
            // Assert
            Assert.Null(result);
            Assert.Null(conflictresult);
        }

        [Fact]
        public async Task CreateAppointment_AddingUnexistingItem_ReturnsType()
        {
            // Act
            var result = await appointmentDAL.CreateAppointmentDALAsync(obj);
            // Assert
            Assert.IsType<Appointment>(result);
        }

        [Fact]
        public async Task UpdateAppointment_UpdateUnexistingItem_ReturnsNull()
        {
            // Act
            await appointmentDAL.CreateAppointmentDALAsync(obj);
            var result = await appointmentDAL.UpdateAppointmentDALAsync(new Appointment());
            var conflictresult = await appointmentDAL.UpdateAppointmentDALAsync(ConflictObj);
            appointmentDAL.DeleteAppointmentDALAsync(obj.Id);
            // Assert
            Assert.Null(result);
            Assert.Null(conflictresult);
        }

        [Fact]
        public async Task UpdateAppointment_UpdateExistingItem_ReturnsNull()
        {
            // Act
            await appointmentDAL.CreateAppointmentDALAsync(obj);
            var result = await appointmentDAL.UpdateAppointmentDALAsync(obj1);
            var sameId = await appointmentDAL.UpdateAppointmentDALAsync(obj2);
            appointmentDAL.DeleteAppointmentDALAsync(obj.Id);
            // Assert
            Assert.IsType<Appointment>(result);
            Assert.IsType<Appointment>(sameId);
        }

        [Fact]
        public async Task DeleteAppointment_DeletingUnexistingItem_ReturnsNull()
        {
            // Act
            var result = appointmentDAL.DeleteAppointmentDALAsync(Guid.NewGuid());
            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteAppointment_DeletingExistingItem_ReturnsType()
        {
            // Act
            var create = await appointmentDAL.CreateAppointmentDALAsync(obj);
            var result = appointmentDAL.DeleteAppointmentDALAsync(obj.Id);
            // Assert
            result.Should().Be(Task.CompletedTask);
        }
    }
}
