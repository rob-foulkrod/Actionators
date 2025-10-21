using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Actionators.Web.Controllers;
using Actionators.Web.Models;
using Actionators.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Actionators.Tests;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _mockLogger;
    private readonly Mock<IContactMessageRepository> _mockRepository;
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        _mockLogger = new Mock<ILogger<HomeController>>();
        _mockRepository = new Mock<IContactMessageRepository>();
        _controller = new HomeController(_mockLogger.Object, _mockRepository.Object);

        // Setup HttpContext and TempData for the controller
        var httpContext = new DefaultHttpContext();
        var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
        _controller.TempData = tempData;
    }

    [Fact]
    public void Index_ReturnsViewResult()
    {
        // Act
        var result = _controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_ReturnsViewResult()
    {
        // Act
        var result = _controller.Privacy();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Contact_Get_ReturnsViewResult()
    {
        // Act
        var result = _controller.Contact();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Contact_Post_WithValidModel_RedirectsToContact()
    {
        // Arrange
        var contactMessage = new ContactMessage
        {
            Name = "John Doe",
            Email = "john@example.com",
            Subject = "Test Subject",
            Message = "This is a test message with sufficient length"
        };

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<ContactMessage>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Contact(contactMessage);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Contact", redirectResult.ActionName);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<ContactMessage>()), Times.Once);
    }

    [Fact]
    public async Task Contact_Post_WithInvalidModel_ReturnsViewWithModel()
    {
        // Arrange
        var contactMessage = new ContactMessage
        {
            Name = "John Doe",
            Email = "invalid-email",
            Subject = "Test",
            Message = "Short"
        };

        _controller.ModelState.AddModelError("Email", "Invalid email");

        // Act
        var result = await _controller.Contact(contactMessage);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(contactMessage, viewResult.Model);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<ContactMessage>()), Times.Never);
    }

    [Fact]
    public void Error_ReturnsViewResultWithErrorViewModel()
    {
        // Act
        var result = _controller.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<ErrorViewModel>(viewResult.Model);
    }
}
