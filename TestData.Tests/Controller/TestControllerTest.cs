using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestData.Controllers;
using TestData.Domain;
using TestData.Domain.HumanDto;
using TestData.Services.Interfaces;

namespace TestData.Tests.Controller;

public class TestControllerTest
{
    private readonly IHumanService _human;
    private readonly ILogger<TestController> _logger;

    public TestControllerTest()
    {
        _human = A.Fake<IHumanService>();
        _logger = A.Fake<ILogger<TestController>>();
    }

    [Fact]
    public async void TestController_GetHumans_ReturnOK()
    {
        //Arrange
        var controller = new TestController(_human, _logger);

        //Act
        var result = await controller.GetData();
        

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
        
    }

    [Fact]
    public async void TestController_GetById_ReturnOK()
    {
        //Arrange
        var controller = new TestController(_human, _logger);
        
        //Act
        var result = await controller.GetById(Guid.NewGuid().ToString().Substring(0, 6));
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));
    }
}