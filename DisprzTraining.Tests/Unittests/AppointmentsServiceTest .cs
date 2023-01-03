
namespace DisprzTraining.Tests
{
    public class AppointmentsServiceTest
    {
        [Fact]
        public async Task Create_Appointment_Returns_StatusCode_201()
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
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.CreateNewAppointment(data)).ReturnsAsync(true);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.CreateAppointment(data);
            var res = result as CreatedResult;
            //Assert
            Assert.Equal(201, res?.StatusCode);
            // res?.Value.Should().BeEquivalentTo(true);
            Assert.True(res?.Value?.Equals(true));
        }
    
        [Fact]
        public async Task Create_Appointment_Returns_StatusCode_409()
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
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.CreateNewAppointment(data)).ReturnsAsync(false);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.CreateAppointment(data);
            var res = result as ConflictResult;
            //Assert
            Assert.Equal(409,res?.StatusCode);               
        }
         [Fact]
        public async Task Get_All_Appointments_Returns_StatusCode_200()
        {
            //Arrange
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.GetAllAddedAppointments()).ReturnsAsync(MockSample.data);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.GetAppointments();
            var res = result.Result as OkObjectResult;

            //Assert
            Assert.Equal(200, res?.StatusCode);
            Assert.IsType<List<Appointment>>(res?.Value);
        }
  
   /// verify
  
        [Fact]
        public async Task Get_Appointments_Returns_StatusCode_200()
        {
            //Arrange
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.GetAppointments("12-18-2022")).ReturnsAsync(MockSample.data);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.GetAppointmentsForDay("12-18-2022");
            var res = result.Result as OkObjectResult;

            //Assert
            Assert.Equal(200,res?.StatusCode);
            Assert.IsType<List<Appointment>>(res?.Value);
             
        }
    

        [Fact]
        public async Task Remove_Appoitments_Returns_Status_Code_200()
        {
            //Arrange
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.RemoveAppointments(new Guid("eaa24756-3fac-4e46-b4bb-074ff4f5b846"))).ReturnsAsync(true);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.Remove(new Guid("eaa24756-3fac-4e46-b4bb-074ff4f5b846"));
            var res = result as OkObjectResult;
            //Assert
            Assert.Equal(200, res?.StatusCode);   
        }
        [Fact]
        public async Task Remove_Appoitments_Returns_Status_Code_404()
        {
            //Arrange
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.RemoveAppointments(new Guid("eaa24756-3fac-4e46-b4bb-074ff4f6b864"))).ReturnsAsync(false);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.Remove(new Guid("eaa24756-3fac-4e46-b4bb-074ff4f6b864"));
            var res = result as NotFoundObjectResult;
            //Assert
            Assert.Equal(404, res?.StatusCode);

        }
         [Fact]
        public async Task Update_Appoitments_Returns_Status_Code_200()
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
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.UpdateAppointments(data)).ReturnsAsync(true);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.Update(data);
            var res = result as OkObjectResult;
            //Assert
            Assert.Equal(200, res?.StatusCode);
        }
         [Fact]
        public async Task Update_Appoitments_Returns_Status_Code_409()
        {
            //Arrange
            Appointment data= new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"),
              Date="12-18-2022",
              Title="lk",
              Description="lkljkhgfds",
              StartTime=DateTime.Now,
              EndTime=DateTime.Now.AddHours(2),
              };
            var mock = new Mock<IAppointmentsBL>();
            mock.Setup(services => services.UpdateAppointments(data)).ReturnsAsync(false);
            var systemUnderTest = new AppointmentsController(mock.Object);
            //Act
            var result = await systemUnderTest.Update(data);
            var res = result as ConflictResult;
            //Assert
            Assert.Equal(409, res?.StatusCode);

        }

    }
}