using DisprzTraining.Business;
using DisprzTraining.Controllers;
using DisprzTraining.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using DisprzTraining.UnitTests.Fixtures;
using Microsoft.AspNetCore.Http;

namespace DisprzTraining.UnitTests.Controllers;

public class AppointmentControllerTests
{
    [Fact]
    public async Task Get_When_Appointments_Found_Returns_ListOfAppointmentDto()
    {
        // Arrange
        Request request = new Request();
        request.Day = new DateTime();
        var mockAppointment = new Mock<IAppointmentBL>();

        mockAppointment
            .Setup(s => s.GetAsync(request))
            .ReturnsAsync(AppointmentFixture.GetAppointmentDictionary());

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Get(request);

        // Assert
        var okResult = result as ObjectResult;

        Assert.NotNull(okResult);
        Assert.True(okResult is OkObjectResult);
        Assert.IsType<Dictionary<int, List<AppointmentDto>>>(okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

    }

    [Fact]
    public async Task GetByDay_When_No_Appointments_Found_Returns_NotFoundObjectResult_404()
    {
        // Arrange
        Request request = new Request();
        request.Day = new DateTime();
        var mockAppointment = new Mock<IAppointmentBL>();

        mockAppointment
            .Setup(s => s.GetAsync(request))
            .ReturnsAsync(new Dictionary<int, List<AppointmentDto>>());

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Get(request);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);

    }

    [Fact]
    public async Task Post_OnSuccess_Returns_CreatedAtActionResult_201()
    {
        // Arrange
        var mockAppointment = new Mock<IAppointmentBL>();

        Guid id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360");
        string T = "DailySyncUp";
        DateTime Start = new DateTime(2022, 12, 30, 5, 10, 20);
        DateTime End = new DateTime(2022, 12, 30, 6, 10, 20);
        Appointment appointment1 = new Appointment
        {
            Id = id,
            Title = T,
            StartTime = Start,
            EndTime = End
        };
        CreateAppointmentDto dto = new CreateAppointmentDto
        {
            Title = T,
            StartTime = Start,
            EndTime = End
        };

        mockAppointment
            .Setup(s => s.CreateAsync(It.IsAny<CreateAppointmentDto>()))
            .ReturnsAsync(appointment1.AsDto());

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Post(dto);

        // Assert
        var okResult = result as ObjectResult;

        Assert.NotNull(okResult);
        Assert.True(okResult is CreatedAtActionResult);
        Assert.Equal(StatusCodes.Status201Created, okResult.StatusCode);

    }

    [Fact]
    public async Task Post_When_There_Is_Conflict_Returns_ConflictResult_409()
    {
        // Arrange
        var mockAppointment = new Mock<IAppointmentBL>();

        Guid id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360");
        string T = "DailySyncUp";
        DateTime Start = new DateTime(2022, 12, 30, 5, 10, 20);
        DateTime End = new DateTime(2022, 12, 30, 6, 10, 20);
        Appointment appointment1 = new Appointment
        {
            Id = id,
            Title = T,
            StartTime = Start,
            EndTime = End
        };
        CreateAppointmentDto dto = new CreateAppointmentDto
        {
            Title = T,
            StartTime = Start,
            EndTime = End
        };

        mockAppointment
            .Setup(s => s.CreateAsync(It.IsAny<CreateAppointmentDto>()))
            .ReturnsAsync(() => null);

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Post(dto);

        // Assert

        result.Should().BeOfType<ConflictResult>();// 1

        var objectResult = (ConflictResult)result;

        objectResult.StatusCode.Should().Be(409);// 2
    }

    [Fact]
    public async Task Post_When_Endtime_Earlier_Than_Starttime_Returns_BadRequest()
    {
        // Arrange
        Guid id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360");
        string title = "DailySyncUp";
        DateTime Start = new DateTime(2022, 12, 31, 10, 10, 20);
        DateTime End = new DateTime(2022, 12, 30, 11, 10, 20);
        CreateAppointmentDto dto = new CreateAppointmentDto
        {
            Title = title,
            StartTime = Start,
            EndTime = End
        };
        AppointmentDto appointment = new AppointmentDto
        {
            Id = id,
            Title = title,
            StartTime = Start,
            EndTime = End
        };
        var mockAppointment = new Mock<IAppointmentBL>();

        mockAppointment
            .Setup(s => s.CreateAsync(dto))
            .ReturnsAsync(appointment);

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Post(dto);

        // Assert
        Assert.IsType<BadRequestResult>(result);

    }

    [Fact]
    public async Task Put_OnSuccess_Returns_CreatedAtActionResult_201()
    {
        // Arrange

        Guid id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360");
        string title = "DailySyncUp";
        DateTime Start = new DateTime(2022, 12, 30, 10, 10, 20);
        DateTime End = new DateTime(2022, 12, 30, 11, 10, 20);

        AppointmentDto appointment = new AppointmentDto
        {
            Id = id,
            Title = title,
            StartTime = Start,
            EndTime = End
        };

        var mockAppointment = new Mock<IAppointmentBL>();

        mockAppointment
            .Setup(s => s.UpdateAsync(appointment))
            .ReturnsAsync(appointment);

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Put(appointment);

        // Assert

        result.Should().BeOfType<CreatedAtActionResult>();// 1

        var objectResult = (CreatedAtActionResult)result;

        objectResult.StatusCode.Should().Be(201);// 2
    }

    [Fact]
    public async Task Put_When_There_Is_Conflict_Returns_ConflictResult_409()
    {
        // Arrange

        Guid id = new Guid("8d6812c7-348b-419f-b6f9-d626b6c1d360");
        string title = "DailySyncUp";
        DateTime Start = new DateTime(2022, 12, 30, 10, 10, 20);
        DateTime End = new DateTime(2022, 12, 30, 11, 10, 20);

        AppointmentDto appointment = new AppointmentDto
        {
            Id = id,
            Title = title,
            StartTime = Start,
            EndTime = End
        };

        var mockAppointment = new Mock<IAppointmentBL>();

        mockAppointment
            .Setup(s => s.UpdateAsync(appointment))
            .ReturnsAsync(() => null);

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Put(appointment);

        // Assert
        result.Should().BeOfType<ConflictResult>();// 1

        var objectResult = (ConflictResult)result;

        objectResult.StatusCode.Should().Be(409);// 2
    }

    [Fact]
    public async Task Delete_OnSuccess_Returns_NoContentResult_204()
    {
        // Arrange
        var mockAppointment = new Mock<IAppointmentBL>();
        Guid id = new Guid();

        mockAppointment
            .Setup(s => s.Delete(id))
            .Returns(Task.FromResult(true));

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Delete(id);

        // Assert
        result.Should().BeOfType<NoContentResult>();// 1

        var objectResult = (NoContentResult)result;

        objectResult.StatusCode.Should().Be(204);// 2
    }

    [Fact]
    public async Task Delete_When_Appointment_Not_Found_Returns_NotFoundResult_404()
    {
        // Arrange
        var mockAppointment = new Mock<IAppointmentBL>();
        Guid id = new Guid();

        mockAppointment
            .Setup(s => s.Delete(id))
            .Returns(Task.FromResult(false));

        var sut = new AppointmentsController(mockAppointment.Object);

        // Act
        var result = await sut.Delete(id);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();// 1

        var objectResult = (NotFoundObjectResult)result;

        objectResult.StatusCode.Should().Be(404);// 2
    }
}