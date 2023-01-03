using DisprzTraining.DataAccess;
using DisprzTraining.Models;
using FluentAssertions;

namespace DisprzTraining.Tests.Systems.DataAccess
{
    public class AppointmentDALTest
    {
        [Fact]
        public async Task GetppointmentByIdAsync_withExistingId_ReturnsAppointment()
        {
            //Arrange
            var id = AppointmentDAL.allAppointments[0].id;
            var sut = new AppointmentDAL();

            //Act
            var res = await ((IAppointmentDAL)sut).GetAppointmentByIdAsync(id);

            //Assert
            res.Should().BeEquivalentTo(
                   AppointmentDAL.allAppointments[0],
                   options => options.Including(o => o.id).Including(o => o.startDate).Including(o => o.endDate).Including(o => o.appointment)
            );
        }

        [Fact]
        public async Task GetppointmentByIdAsync_withInValidId_ReturnsAppointment()
        {
            //Arrange
            Guid id = new Guid();
            var sut = new AppointmentDAL();

            //Act
            var res = await ((IAppointmentDAL)sut).GetAppointmentByIdAsync(id);

            //Assert
            Assert.Equal(null, res);
        }

        [Fact]
        public async Task GetppointmentByDateAsync_withValidDate_ReturnsListOfAppointment()
        {
            //Arrange
            DateTime d = new DateTime(2022, 12, 21);
            var sut = new AppointmentDAL();

            //Act
            var res = await sut.GetAppointmentsByDateAsync(d);

            //Assert
            // res.Should().BeOfType<List<Appointment>>();
            Assert.IsType<List<Appointment>>(res);
        }

        [Fact]
        public async Task GetppointmentByDateAsync_withInValidDate_ReturnsNull()
        {
            //Arrange
            DateTime d = new DateTime();
            var sut = new AppointmentDAL();

            //Act
            var res = await sut.GetAppointmentsByDateAsync(d);

            //Assert
            // res.Should().BeEmpty();
            Assert.Empty(res);
        }

        [Fact]
        public async Task AddAppointmentAsync_withExistingAppointmentToAdd_ReturnsFalse()
        {
            //Arrange
            var existingAppointment = AppointmentDAL.allAppointments[0];
            var sut = new AppointmentDAL();

            //Act
            var res = await sut.AddAppointmentAsync(existingAppointment);

            //Assert
            Assert.Equal(false, res);
        }

        [Fact]
        public async Task AddAppointmentAsync_withNewAppointmentToAdd_ReturnsTrue()
        {
            //Arrange
            Appointment singleAppointment = new Appointment() { id = Guid.NewGuid(), startDate = new DateTime(2023, 12, 21, 9, 0, 0), endDate = new DateTime(2023, 12, 21, 10, 0, 0), appointment = "BLTest" };
            var sut = new AppointmentDAL();

            //Act
            var res = await sut.AddAppointmentAsync(singleAppointment);

            //Assert
            Assert.Equal(true, res);
        }

        [Fact]
        public async Task DelteAppointmentAsync_withInvalidId_ReturnsFalse()
        {
            //Arrange
            Guid id = Guid.Empty;
            var sut = new AppointmentDAL();

            //Act
            var res = await sut.DeleteAppointmentAsync(id);

            //Assert
            Assert.Equal(false, res);
        }

        [Fact]
        public async Task DelteAppointmentAsync_withValidId_ReturnsTrue()
        {
            //Arrange
            var id = AppointmentDAL.allAppointments[0].id;
            var sut = new AppointmentDAL();

            //Act
            var res = await sut.DeleteAppointmentAsync(id);

            //Assert
            Assert.Equal(true, res);
        }
    }
}