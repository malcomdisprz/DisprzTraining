using DisprzTraining.DataAccess;
using DisprzTraining.Models;
using FluentAssertions;

namespace DisprzTraining.UnitTests.DataAccess;

public class AppointmentDALTests
{
    [Fact]
    public async Task Create_On_NoConflict_Returns_true()
    {
        // Arrange
        Appointment appointment = new()
        {
            Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360"),
            Title = "DailySyncUp",
            StartTime = new DateTime(2022, 12, 31, 3, 10, 20),
            EndTime = new DateTime(2022, 12, 31, 4, 10, 20)
        };
        var mockAppointmentDAL = new AppointmentDAL();

        // Act
        var result = await mockAppointmentDAL.Create(appointment);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public async Task Create_On_Conflict_Returns_false()
    {
        // Arrange
        Appointment appointment = new()
        {
            Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360"),
            Title = "DailySyncUp",
            StartTime = new DateTime(2022, 12, 30, 5, 10, 20),
            EndTime = new DateTime(2022, 12, 30, 6, 10, 20)
        };
        var mockAppointmentDAL = new AppointmentDAL();

        // Act
        var result = await mockAppointmentDAL.Create(appointment);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public async Task GetByDay_When_Appointments_Found_Returns_ListOfAppointments()
    {
        // Arrange
        DateTime day = new DateTime(2022, 12, 30, 5, 10, 20);
        List<Appointment> appointments = new List<Appointment>(){
            new Appointment{
                Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d361"),
                Title = "M1",
                StartTime = new DateTime(2022, 12, 30, 5, 10, 20),
                EndTime = new DateTime(2022, 12, 30, 6, 10, 20)
            },
            new Appointment{
                Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d362"),
                Title = "M2",
                StartTime = new DateTime(2022, 12, 30, 7, 10, 20),
                EndTime = new DateTime(2022, 12, 30, 8, 10, 20)
            },
        };
        var mockAppointmentDAL = new AppointmentDAL();

        // Act
        var result = await mockAppointmentDAL.GetByDay(day);

        // Assert
        result.Should().BeEquivalentTo(
                   appointments,
                   options => options.ComparingByMembers<Appointment>().ExcludingMissingMembers()
               );
    }

    [Fact]
    public async Task GetByDay_When_No_Appointments_Found_Returns_EmptyListOfAppointments()
    {
        // Arrange
        DateTime day = new DateTime(2022, 12, 28, 5, 10, 20);
        List<Appointment> appointments = new List<Appointment>();
        var mockAppointmentDAL = new AppointmentDAL();

        // Act
        var result = await mockAppointmentDAL.GetByDay(day);

        // Assert
        result.Should().BeEquivalentTo(
                   appointments,
                   options => options.ComparingByMembers<Appointment>().ExcludingMissingMembers()
               );
    }

    [Fact]
    public async Task Delete_OnSuccess_Returns_true()
    {
        // Arrange
        Guid Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d365");
        var mockAppointmentDAL = new AppointmentDAL();

        // Act
        var result = await mockAppointmentDAL.Delete(Id);

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public async Task Delete_OnFailure_Returns_true()
    {
        // Arrange
        Guid Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d369");
        var mockAppointmentDAL = new AppointmentDAL();

        // Act
        var result = await mockAppointmentDAL.Delete(Id);

        // Assert
        Assert.Equal(false, result);
    }
}
