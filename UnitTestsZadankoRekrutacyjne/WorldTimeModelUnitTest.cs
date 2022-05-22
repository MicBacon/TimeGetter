using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ZadankoRekrutacyjne.Models;

namespace UnitTestZadankoRekrutacyjne;

public class Tests
{
    [Test]
    public void Getting_Time_From_Json_Data_Property()
    {
        // arrange
        var wtm = new WorldTimeModel
        {
            JsonData = @"{""abbreviation"":""WEST"",""client_ip"":""2a02:a312:c341:2880:3ca3:9d3a:c3e6:e37f"",""datetime"":""2022-05-22T21:58:02.330596+01:00"",""day_of_week"":0,""day_of_year"":142,""dst"":true,""dst_from"":""2022-03-27T01:00:00+00:00"",""dst_offset"":3600,""dst_until"":""2022-10-30T01:00:00+00:00"",""raw_offset"":0,""timezone"":""WET"",""unixtime"":1653253082,""utc_datetime"":""2022-05-22T20:58:02.330596+00:00"",""utc_offset"":""+01:00"",""week_number"":20}"
        };

        // act
        var time = wtm.GetTime();
        
        // assert
        Assert.That(time, Does.Match(@"^(?:(?:([01]?\d|2[0-3]):)?([0-5]?\d):)?([0-5]?\d)$"));
    }
    
    [Test]
    public void Getting_Available_Time_Zones_List()
    {
        // arrange
        var wtm = new WorldTimeModel
        {
            JsonData = @"[""Europe/Amsterdam"",""Europe/Andorra"",""Europe/Astrakhan"",""Europe/Athens"",""Europe/Belgrade"",""Europe/Berlin"",""Europe/Brussels"",""Europe/Bucharest"",""Europe/Budapest"",""Europe/Chisinau"",""Europe/Copenhagen"",""Europe/Dublin"",""Europe/Gibraltar"",""Europe/Helsinki"",""Europe/Istanbul"",""Europe/Kaliningrad"",""Europe/Kiev"",""Europe/Kirov"",""Europe/Lisbon"",""Europe/London"",""Europe/Luxembourg"",""Europe/Madrid"",""Europe/Malta"",""Europe/Minsk"",""Europe/Monaco"",""Europe/Moscow"",""Europe/Oslo"",""Europe/Paris"",""Europe/Prague"",""Europe/Riga"",""Europe/Rome"",""Europe/Samara"",""Europe/Saratov"",""Europe/Simferopol"",""Europe/Sofia"",""Europe/Stockholm"",""Europe/Tallinn"",""Europe/Tirane"",""Europe/Ulyanovsk"",""Europe/Uzhgorod"",""Europe/Vienna"",""Europe/Vilnius"",""Europe/Volgograd"",""Europe/Warsaw"",""Europe/Zaporozhye"",""Europe/Zurich""]"
        };

        // act
        var timeZonesList = wtm.GetTimeZonesList();
        var expectedList = new List<string>
        {
            "Europe/Amsterdam",
            "Europe/Andorra",
            "Europe/Astrakhan",
            "Europe/Athens",
            "Europe/Belgrade",
            "Europe/Berlin",
            "Europe/Brussels",
            "Europe/Bucharest",
            "Europe/Budapest",
            "Europe/Chisinau",
            "Europe/Copenhagen",
            "Europe/Dublin",
            "Europe/Gibraltar",
            "Europe/Helsinki",
            "Europe/Istanbul",
            "Europe/Kaliningrad",
            "Europe/Kiev",
            "Europe/Kirov",
            "Europe/Lisbon",
            "Europe/London",
            "Europe/Luxembourg",
            "Europe/Madrid",
            "Europe/Malta",
            "Europe/Minsk",
            "Europe/Monaco",
            "Europe/Moscow",
            "Europe/Oslo",
            "Europe/Paris",
            "Europe/Prague",
            "Europe/Riga",
            "Europe/Rome",
            "Europe/Samara",
            "Europe/Saratov",
            "Europe/Simferopol",
            "Europe/Sofia",
            "Europe/Stockholm",
            "Europe/Tallinn",
            "Europe/Tirane",
            "Europe/Ulyanovsk",
            "Europe/Uzhgorod",
            "Europe/Vienna",
            "Europe/Vilnius",
            "Europe/Volgograd",
            "Europe/Warsaw",
            "Europe/Zaporozhye",
            "Europe/Zurich"
        };
        
        // assert
        Assert.That(timeZonesList, Has.All.Matches<string>(f => IsInExpected(f, expectedList)));
    }
    
    private static bool IsInExpected(string item, IEnumerable<string> expected)
    {
        var matchedItem = expected.FirstOrDefault(f => 
            f == item
        );

        return matchedItem != null;
    }
    
}