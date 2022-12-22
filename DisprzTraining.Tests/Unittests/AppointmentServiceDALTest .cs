namespace DisprzTraining.Tests
{
    
    public class AppointmentServiceDALTest 
    {

    [Fact]
    public async Task Create_New_Appointment_Returns_True()
    {
      //Arrange
      AddNewAppointment data = new AddNewAppointment()
            {
                Date = "18-12-2022",
                Title = "test",
                Description = "test-case",
                StartTime = DateTime.Now.AddMinutes(30),
                EndTime = DateTime.Now.AddHours(1),
            };
      var systemUnderTest=new AppointmentsDAL();

      //Act
      var result=await systemUnderTest.CreateNewAppointments(data);

      //Assert
      Assert.True(result);
    }

    [Fact]
    public async Task Get_All_Appointments_Returns_List()
    {
      //Arrange
      var systemUnderTest=new AppointmentsDAL();

      //Act
      var result=await systemUnderTest.GetAllAppointments();

      //Assert
      Assert.IsType<List<Appointment>>(result);
    }

     [Fact]
    public async Task Get_Appointments_By_Date_Returns_List()
    {
      //Arrange
      var systemUnderTest=new AppointmentsDAL();

      //Act
      var result=await systemUnderTest.GetAppointmentsByDate("18-12-2022");

      //Assert
      Assert.IsType<List<Appointment>>(result);
    }
    [Fact]
    public async Task Remove_Appointment_By_Id_Returns_True()
    {
    
      //Arrange
      var systemUnderTest=new AppointmentsDAL();

      //Act
     
      var result=await systemUnderTest.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"));

      //Assert
      Assert.True(result);
    }

     [Fact]
    public async Task Remove_Appointment_By_Id_Returns_False()
    {
    
      //Arrange
      var systemUnderTest=new AppointmentsDAL();

      //Act
     
      var result=await systemUnderTest.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"));

      //Assert
      Assert.False(result);
    }

     [Fact]
    public async Task Update_Appointment_Returns_False_Whn_Id_is_Wrong()
    {
      Appointment data= new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"),
              Date="21-12-2022",
              Title="Lalith-updated",
              Description="test",
              StartTime=DateTime.Now,
              EndTime=DateTime.Now.AddHours(2),
              };
      var systemUnderTest=new AppointmentsDAL();

      //Act
      
      var result=await systemUnderTest.UpdateAppointmentById(data);

      //Assert
      Assert.False(result);
    }

     [Fact]
    public async Task Update_Appointment_Returns_False_When_Id_is_Wrong()
    {
      Appointment data= new Appointment(){
              Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"),
              Date="21-12-2022",
              Title="Lalith-updated",
              Description="test",
              StartTime=DateTime.Now,
              EndTime=DateTime.Now.AddHours(2),
              };
      var systemUnderTest=new AppointmentsDAL();

      //Act
      
      var result=await systemUnderTest.UpdateAppointmentById(data);

      //Assert
      Assert.False(result);
    }


    }
}