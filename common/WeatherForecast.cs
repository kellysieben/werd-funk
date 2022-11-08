namespace common;
public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary
    {
        get
        {
            var summary = "Mild";

            if (TemperatureC >= 32)
            {
                summary = "Hot";
            }
            else if (TemperatureC <= 16 && TemperatureC > 0)
            {
                summary = "Cold";
            }
            else if (TemperatureC <= 0)
            {
                summary = "Freezing!";
            }

            return summary;
        }
    }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
