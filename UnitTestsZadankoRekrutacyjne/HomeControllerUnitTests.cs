using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ZadankoRekrutacyjne.Controllers;

namespace UnitTestZadankoRekrutacyjne;

public class HomeControllerUnitTests
{
    [Test]
    public void Index_As_ViewResult()
    {
        // arrange
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables() 
            .Build();
        
        var hc = new HomeController(config);

        // act
        var vTask = hc.Index();
        vTask.Wait();

        var vr = vTask.Result as ViewResult;

        // assert
        Assert.NotNull(vr);
        Assert.AreEqual("Index", vr.ViewName);
    }
}