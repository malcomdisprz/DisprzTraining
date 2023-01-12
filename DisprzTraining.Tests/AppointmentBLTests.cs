using DisprzTraining.UnitTests.Fixtures;
using DisprzTraining.Business;
using DisprzTraining.DataAccess;
using DisprzTraining.Models;
using FluentAssertions;
using Moq;

namespace DisprzTraining.UnitTests.Business;

public class AppointmentBLTests
{

    [Fact]
    public async Task GetAsync_WhenCalled_Returns_ListOfAppointmentDtos()
    {
        // Arrange
        Request request = new Request();
        request.Day = new DateTime();
        var appointments = AppointmentFixture.GetAppointments();
        var appointmentDtos = (appointments.Select(s => s.AsDto())).ToList();
        var mockAppointmentDAL = new Mock<IAppointmentDAL>();

        mockAppointmentDAL
            .Setup(s => s.Get(request))
            .Returns(Task.FromResult(appointments));

        var sut = new AppointmentBL(mockAppointmentDAL.Object);

        // Act
        var result = await sut.GetAsync(request);

        // Assert
        result.Should().BeOfType<Dictionary<int, List<AppointmentDto>>>();// 1
    }

    [Fact]
    public async Task Delete_WhenCalled_Returns_Boolean()
    {
        // Arrange
        var boo = false;
        Guid id = new Guid();
        var mockAppointmentBL = new Mock<IAppointmentDAL>();

        mockAppointmentBL
            .Setup(s => s.Delete(id))
            .Returns(Task.FromResult(boo));

        var sut = new AppointmentBL(mockAppointmentBL.Object);

        // Act
        var result = await sut.Delete(id);

        // Assert
        Assert.Equal(boo, result);
    }

    [Fact]
    public async Task CreateAsync_OnSuccess_Returns_Created_Appointment()
    {
        // Arrange
        CreateAppointmentDto appointmentDto = new CreateAppointmentDto
        {
            Title = "A1",
            StartTime = new DateTime(2022, 12, 30, 5, 10, 20),
            EndTime = new DateTime(2022, 12, 30, 6, 10, 20)
        };
        var mockAppointmentBL = new Mock<IAppointmentDAL>();
        mockAppointmentBL
            .Setup(s => s.Create(It.IsAny<Appointment>()))
            .Returns(Task.FromResult(true));

        var sut = new AppointmentBL(mockAppointmentBL.Object);

        // Act
        var result = await sut.CreateAsync(appointmentDto);

        // Assert
        result.Should().BeOfType<AppointmentDto>();// 1
    }


    [Fact]
    public async Task CreateAsync_OnFailure_Returns_Null()
    {
        // Arrange
        CreateAppointmentDto appointmentDto = new CreateAppointmentDto
        {
            Title = "A1",
            StartTime = new DateTime(2022, 12, 30, 5, 10, 20),
            EndTime = new DateTime(2022, 12, 30, 6, 10, 20)
        };
        var mockAppointmentBL = new Mock<IAppointmentDAL>();
        mockAppointmentBL
            .Setup(s => s.Create(It.IsAny<Appointment>()))
            .Returns(Task.FromResult(false));

        var sut = new AppointmentBL(mockAppointmentBL.Object);

        // Act
        var result = await sut.CreateAsync(appointmentDto);

        // Assert
        result.Should().Be(null);
    }

    [Fact]
    public async Task UpdateAsync_OnSuccess_Returns_Updated_Appointment()
    {
        // Arrange
        AppointmentDto appointmentDto = new AppointmentDto
        {
            Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360"),
            Title = "A1",
            StartTime = new DateTime(2022, 12, 30, 5, 10, 20),
            EndTime = new DateTime(2022, 12, 30, 6, 10, 20)
        };
        var mockAppointmentBL = new Mock<IAppointmentDAL>();
        mockAppointmentBL
            .Setup(s => s.Update(It.IsAny<Appointment>()))
            .Returns(Task.FromResult(true));

        var sut = new AppointmentBL(mockAppointmentBL.Object);

        // Act
        var result = await sut.UpdateAsync(appointmentDto);

        // Assert
        result.Should().BeOfType<AppointmentDto>();// 1
    }

    [Fact]
    public async Task UpdateAsync_OnFailure_Returns_Null()
    {
        // Arrange
        AppointmentDto appointmentDto = new AppointmentDto
        {
            Id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360"),
            Title = "A1",
            StartTime = new DateTime(2022, 12, 30, 5, 10, 20),
            EndTime = new DateTime(2022, 12, 30, 6, 10, 20)
        };
        var mockAppointmentBL = new Mock<IAppointmentDAL>();
        mockAppointmentBL
            .Setup(s => s.Create(It.IsAny<Appointment>()))
            .Returns(Task.FromResult(false));

        var sut = new AppointmentBL(mockAppointmentBL.Object);

        // Act
        var result = await sut.UpdateAsync(appointmentDto);

        // Assert
        result.Should().Be(null);
    }
}