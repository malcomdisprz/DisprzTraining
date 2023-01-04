
namespace DisprzTraining.Tests
{
    public class AppointmentServiceBLTest
    {
        [Fact]
        public async Task Create_NewAppointment_Appointment_IS_Created()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = "12-18-2022",
                Title = "test",
                Description = "test-case",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
            };
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.CreateNewAppointments(data)).ReturnsAsync(true);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
            var result =  await systemUnderTest.CreateNewAppointment(data);
            
            //Assert
             Assert.True(result);
        }
         [Fact]
        public async Task Create_NewAppointment_Not_Created()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = "12-18-2022",
                Title = "test",
                Description = "test-case",
                StartTime = DateTime.Now.AddMinutes(30),
                EndTime = DateTime.Now.AddHours(1),
            };
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.CreateNewAppointments(data)).ReturnsAsync(false);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
            var result = await systemUnderTest.CreateNewAppointment(data);
            
            //Assert
            Assert.False(result);
           
        }
       [Fact]
        public async Task Create_NewAppointment_Throws_Exception()
        {
            //Arrange
            AddNewAppointment data = new AddNewAppointment()
            {
                Date = "12-18-2022",
                Title = "test",
                Description = "test-case",
                 StartTime=new DateTime(2022,12,21,11,0,0),
                 EndTime=new DateTime(2022,12,21,11,0,0),
            };
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.CreateNewAppointments(data)).ThrowsAsync(new Exception());
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Assert
            Assert.ThrowsAnyAsync<Exception>(()=>systemUnderTest.CreateNewAppointment(data));
    
            
        }

          [Fact]
        public async Task Get_All_Added_Appointments_Returns_List()
        {
            //Arrange
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.GetAllAppointments()).ReturnsAsync(MockSample.data);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
            var result = await systemUnderTest.GetAllAddedAppointments();
            //Assert
            Assert.Equal((MockSample.data).Count,(result).Count);
        }
        [Fact]
         public async Task Get_Appointments_Returns_List()
        {
            //Arrange
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.GetAppointmentsByDate("12-18-2022")).ReturnsAsync(MockSample.data);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
            var result = await systemUnderTest.GetAppointments("12-18-2022");
            //Assert
            Assert.IsType<List<Appointment>>(result);
           Assert.Equal((MockSample.data).Count,result.Count);
        }
        [Fact]
        public async Task Remove_Appointments_Returns_True()
        {
            //Arrange
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"))).ReturnsAsync(true);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
            var result = await systemUnderTest.RemoveAppointments(new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"));
            //Assert
            Assert.True(result); 
        }

         [Fact]
        public async Task Remove_Appointments_Returns_False()
        {
            //Arrange
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"))).ReturnsAsync(false);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
            var result = await systemUnderTest.RemoveAppointments(new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"));
            //Assert
            Assert.False(result); 
        }
        
        [Fact]
        public async Task Update_Appoitments_Returns_True()
        {
            //Arrange
            Appointment data= new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
              Date="12-18-2022",
              Title="lk",
              Description="test",
              StartTime=DateTime.Now,
              EndTime=DateTime.Now.AddHours(2),
              };
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.UpdateAppointmentById(data)).ReturnsAsync(true);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
            
            var result = await systemUnderTest.UpdateAppointments(data);
            
            //Assert
            Assert.True(result);
        }
        [Fact]

         public async Task Update_Appoitments_Returns_False_When_Id_Is_Wrong()
        {
            //Arrange
            Appointment data= new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90a56"),
              Date="12-19-2022",
              Title="lk",
              Description="test",
              StartTime=DateTime.Now,
              EndTime=DateTime.Now.AddHours(2),
              };
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.UpdateAppointmentById(data)).ReturnsAsync(false);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
             var result=await systemUnderTest.UpdateAppointments(data);
        
        
            // Assert.False(result);
            Assert.False(result);
        }
        [Fact]

         public async Task Update_Appoitments_Returns_False_When_Conflict()
        {
            //Arrange
            Appointment data= new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f80ae7"),
              Date="12-21-2022",
              Title="lk-conflict",
              Description="test-scenarios",
              StartTime=new DateTime(2022,12,21,11,30,0),
              EndTime=new DateTime(2022,12,21,12,0,0),
              };
            var mock = new Mock<IAppointmentsDAL>();
            mock.Setup(services => services.UpdateAppointmentById(data)).ReturnsAsync(false);
            var systemUnderTest = new AppointmentsBL(mock.Object);
            //Act
             var result=await systemUnderTest.UpdateAppointments(data);
            Assert.False(result);
        }
        
    }
}