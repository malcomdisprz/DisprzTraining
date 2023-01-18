
namespace DisprzTraining.Tests
{
    public class AppointmentServiceBLTest
    {
        // [Fact]
        // public async Task Create_NewAppointment_Appointment_IS_Created()
        // {
        //     //Arrange
        //     AddNewAppointment data = new AddNewAppointment()
        //     {
        //         Date = new DateTime(2022, 12, 21, 0, 0, 0),
        //         Title = "test",
        //         Description = "test-case",
        //         Type = "reminder",
        //         StartTime = new DateTime(2022, 12, 21, 6, 0, 0),
        //         EndTime = new DateTime(2022, 12, 21, 7, 0, 0),
        //     };
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.CreateNewAppointments(data)).ReturnsAsync(true);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act
        //     var result = await systemUnderTest.CreateNewAppointment(data);

        //     //Assert
        //     Assert.True(result);
        // }
        // [Fact]
        // public async Task Create_NewAppointment_Not_Created()
        // {
        //     //Arrange
        //     AddNewAppointment data = new AddNewAppointment()
        //     {
        //         Date = new DateTime(2022, 12, 21, 0, 0, 0),
        //         Title = "test",
        //         Description = "test-case",
        //         Type = "reminder",
        //         StartTime = new DateTime(2022, 12, 21, 11, 0, 0),
        //         EndTime = new DateTime(2022, 12, 21, 12, 0, 0),
        //     };
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.CreateNewAppointments(data)).ReturnsAsync(false);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act
        //     var result = await systemUnderTest.CreateNewAppointment(data);
            
        //     //Assert
        //     Assert.False(result);
        // }
        [Fact]
        public async Task Create_NewAppointment_Throws_Exception_When_End_Time_Is_Lesser()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = DateTime.Now,
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime =DateTime.Now.AddHours(2),
                EndTime = DateTime.Now.AddHours(1),
            };
              IAppointmentsDAL appointmentsDAL=new AppointmentsDAL();
              IAppointmentsBL appointmentsBL=new AppointmentsBL(appointmentsDAL);
              
            //Assert
            Assert.Throws<Exception>(() => appointmentsBL.CreateNewAppointment(data) );

        }
        [Fact]
        public async Task Create_NewAppointment_Throws_Exception_StartTime_EndTime_Are_Same()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = DateTime.Now,
                Title = "test",
                Description = "test-case",
                Type = "reminder",
                StartTime =DateTime.Now,
                EndTime = DateTime.Now,
            };
              IAppointmentsDAL appointmentsDAL=new AppointmentsDAL();
              IAppointmentsBL appointmentsBL=new AppointmentsBL(appointmentsDAL);
              
            //Assert
            Assert.Throws<Exception>(() => appointmentsBL.CreateNewAppointment(data) );

        }

        // [Fact]
        // public async Task Get_All_Added_Appointments_Returns_List()
        // {
        //     //Arrange
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.GetAllAppointments()).ReturnsAsync(MockSample.data);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act
        //     var result = await systemUnderTest.GetAllAddedAppointments();
        //     //Assert
        //     Assert.Equal((MockSample.data).Count, (result).Count);
        // }
        // [Fact]
        // public async Task Get_Appointments_Returns_List()
        // {
        //     DateTime date = new DateTime(2022, 12, 18, 0, 0, 0);
        //     //Arrange
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.GetAppointmentsByDate(date)).ReturnsAsync(MockSample.data);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act
        //     var result = await systemUnderTest.GetAppointments(date);
        //     //Assert
        //     Assert.IsType<List<Appointment>>(result);
        //     Assert.Equal((MockSample.data).Count, result.Count);
        // }
        // [Fact]
        // public async Task Remove_Appointments_Returns_True()
        // {
        //     //Arrange
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"))).ReturnsAsync(true);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act
        //     var result = await systemUnderTest.RemoveAppointments(new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"));
        //     //Assert
        //     Assert.True(result);
        // }

        // [Fact]
        // public async Task Remove_Appointments_Returns_False()
        // {
        //     //Arrange
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"))).ReturnsAsync(false);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act
        //     var result = await systemUnderTest.RemoveAppointments(new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"));
        //     //Assert
        //     Assert.False(result);
        // }

        // [Fact]
        // public async Task Update_Appoitments_Returns_True()
        // {
        //     //Arrange
        //     Appointment data = new Appointment()
        //     {
        //         Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
        //         Date = new DateTime(2022, 12, 18, 0, 0, 0),
        //         Title = "lk",
        //         Description = "test",
        //         Type = "reminder",
        //         StartTime = new DateTime(2022, 12, 18, 11, 0, 0),
        //         EndTime = new DateTime(2022, 12, 18, 12, 0, 0),
        //     };
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.UpdateAppointmentById(data)).ReturnsAsync(true);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act

        //     var result = await systemUnderTest.UpdateAppointments(data);

        //     //Assert
        //     Assert.True(result);
        // }
        // [Fact]

        // public async Task Update_Appoitments_Returns_False_When_Id_Is_Wrong()
        // {
        //     //Arrange
        //     Appointment data = new Appointment()
        //     {
        //         Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90a56"),
        //         Date = new DateTime(2022, 12, 18, 0, 0, 0),
        //         Title = "lk",
        //         Description = "test",
        //         Type = "reminder",
        //         StartTime = DateTime.Now,
        //         EndTime = DateTime.Now.AddHours(2),
        //     };
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.UpdateAppointmentById(data)).ReturnsAsync(false);
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Act


        // }
        // [Fact]

        // public async Task Update_Appoitments_throws_Exception_When_Conflict()
        // {
        //     //Arrange
        //     Appointment data = new Appointment()
        //     {
        //         Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f80ae7"),
        //         Date = new DateTime(2022, 12, 18, 0, 0, 0),
        //         Title = "lk-conflict",
        //         Description = "test-scenarios",
        //         Type = "reminder",
        //         StartTime = new DateTime(2022, 12, 21, 11, 30, 0),
        //         EndTime = new DateTime(2022, 12, 21, 11, 0, 0),
        //     };
        //     var mock = new Mock<IAppointmentsDAL>();
        //     mock.Setup(services => services.UpdateAppointmentById(data)).ThrowsAsync(new Exception());
        //     var systemUnderTest = new AppointmentsBL(mock.Object);
        //     //Assert

        //     mock.Setup(services => services.UpdateAppointmentById(data)).ReturnsAsync(false);

        //     //Act

        //     Assert.ThrowsAnyAsync<Exception>(() => systemUnderTest.UpdateAppointments(data));
        // }

    }
}