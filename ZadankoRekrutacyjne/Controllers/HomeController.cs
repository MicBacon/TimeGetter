using System.Net;
using Microsoft.AspNetCore.Mvc;
using ZadankoRekrutacyjne.Models;

namespace ZadankoRekrutacyjne.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<IActionResult> Index()
    {
        var model = new WorldTimeModel();
        var timeZone = _configuration["TimeZone"];

        model.RequestedTimeZone = timeZone;
        var requestLink = "https://worldtimeapi.org/api/timezone/" + timeZone;
        
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync(requestLink))
            {
                // api not available case (http response code - 410)
                if (response.StatusCode == HttpStatusCode.Gone)
                {
                    model.IsApiAvailable = false;
                    return View(model);
                }
                
                // receive a message from HttpResponse
                string apiResponse = await response.Content.ReadAsStringAsync();
                
                // invalid input case
                if (!apiResponse.Contains("abbreviation"))
                {
                    model.IsInvalidInput = true;
                    
                    // not exising time zone
                    if (apiResponse.Contains("error"))
                    {
                        using var possibleTimeZones =
                            await httpClient.GetAsync("https://worldtimeapi.org/api/timezone/");
                        
                        // api not available case (http response code - 410)
                        if (response.StatusCode == HttpStatusCode.Gone)
                        {
                            model.IsApiAvailable = false;
                            return View(model);
                        }
                        
                        apiResponse = await possibleTimeZones.Content.ReadAsStringAsync();
                    }
                }
                 
                // received json data as string
                model.JsonData = apiResponse;
            }
        }
        
        return View(model);
    }
}