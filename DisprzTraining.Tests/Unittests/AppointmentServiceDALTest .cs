namespace DisprzTraining.Tests
{
    
    public class AppointmentServiceDALTest:IDisposable
    {
        private readonly Appointment _test;
        private bool disposedValue;

        public AppointmentServiceDALTest()
         {
            _test = new Appointment();
         }

      

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~AppointmentServiceDALTest()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
           
        }
        // [Fact]
        // public async Task Create_New_Appointment_Returns_True()
        // {
        //   //Arrange
        //   AddNewAppointment data = new AddNewAppointment()
        //         {
        //             Date = new DateTime(2022,12,18,0,0,0),
        //             Title = "test",
        //             Description = "test-case",
        //             Type="reminder",
        //             StartTime = new DateTime(2022,12,18,1,0,0),
        //             EndTime = new DateTime(2022,12,18,2,0,0),
        //         };
        //   var systemUnderTest=new AppointmentsDAL();

        //   //Act
        //   var result=await systemUnderTest.CreateNewAppointments(data);

        //   //Assert
        //   Assert.True(result);
        // }

        // [Fact]
        // public async Task Get_All_Appointments_Returns_List()
        // {
        //   //Arrange
        //   var systemUnderTest=new AppointmentsDAL();

        //   //Act
        //   var result=await systemUnderTest.GetAllAppointments();

        //   //Assert
        //   Assert.IsType<List<Appointment>>(result);
        // }

        //  [Fact]
        // public async Task Get_Appointments_By_Date_Returns_List()
        // {
        //   DateTime Date=new DateTime(2022,12,18,0,0,0);
        //   //Arrange
        //   var systemUnderTest=new AppointmentsDAL();

        //   //Act
        //   var result=await systemUnderTest.GetAppointmentsByDate(Date);

        //   //Assert
        //   Assert.IsType<List<Appointment>>(result);
        // }
        // [Fact]
        // public async Task Remove_Appointment_By_Id_Returns_True()
        // {

        //   //Arrange
        //   var systemUnderTest=new AppointmentsDAL();

        //   //Act

        //   var result=await systemUnderTest.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"));

        //   //Assert
        //   Assert.True(result);
        // }

        //  [Fact]
        // public async Task Remove_Appointment_By_Id_Returns_False()
        // {

        //   //Arrange
        //   var systemUnderTest=new AppointmentsDAL();

        //   //Act

        //   var result=await systemUnderTest.RemoveAppointmentsById(new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"));

        //   //Assert
        //   Assert.False(result);
        // }

        //  [Fact]
        // public async Task Update_Appointment_Returns_True()
        // {
        //   Appointment data= new Appointment(){
        //           Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"),
        //           Date=new DateTime(2022,12,18,0,0,0),
        //           Title="-updated",
        //           Description="test",
        //           Type="reminder",
        //           StartTime=new DateTime(2022,12,18,21,0,0),
        //           EndTime=new DateTime(2022,12,18,23,0,0),
        //           };
        //   var systemUnderTest=new AppointmentsDAL();

        //   //Act

        //   var result=await systemUnderTest.UpdateAppointmentById(data);

        //   //Assert
        //   Assert.False(result);
        // }

        //  [Fact]
        // public async Task Update_Appointment_Returns_False_When_Id_is_Wrong()
        // {
        //   Appointment data= new Appointment(){
        //           Id=new Guid("7c9e6679-7425-40de-944b-e07fc1f90a7e"),
        //           Date=new DateTime(2022,12,18,0,0,0),
        //           Title="Lalith-updated",
        //           Description="test",
        //           Type="reminder",
        //           StartTime=new DateTime(2022,12,18,0,0,0),
        //           EndTime=DateTime.Now.AddHours(2),
        //           };
        //   var systemUnderTest=new AppointmentsDAL();

        //   //Act

        //   var result=await systemUnderTest.UpdateAppointmentById(data);

        //   //Assert
        //   Assert.False(result);
        // }


    }
}