using System.Net;
using Newtonsoft.Json;
using System.Security.Principal;

namespace WebApplication1.Models;

public struct Weather
{
    public string Country { get; set; }
    public string Name { get; set; }
    public double Temp { get; set; }
    public string Description { get; set; }

    public Weather(WeatherResponse weatherResponse)
    {
        this.Country = weatherResponse.Sys.Country;
        this.Name = weatherResponse.Name;
        this.Temp = weatherResponse.Main.Temp;
        this.Description = weatherResponse.Weather[0].Description;
    }
    
    public void Print()
    {
        Console.Write("country: " + Country + ", ");
        Console.Write("name of place: " + Name + ", ");
        Console.Write("description: " + Description + ", ");
        Console.Write("temperature: " + Temp + "\n");
    }
}


public class WeatherResponse
{
    public TemperatureInfo Main { get; set; }
    public CountryInfo Sys { get; set; }
    public string Name { get; set; }
    public DescriptionInfo[] Weather { get; set; }

    public void Print()
    {
        Console.Write("country: " + Sys.Country + ", ");
        Console.Write("name of place: " + Name + ", ");
        Console.Write("description: " + Weather[0].Description + ", ");
        Console.Write("temperature: " + Main.Temp + "\n");
    }
}

public class TemperatureInfo
{
    public float Temp { get; set; }
}

public class DescriptionInfo
{
    public string Description { get; set; }
}

public class CountryInfo
{
    public string Country { get; set; }
}