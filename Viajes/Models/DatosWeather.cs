namespace Viajes.Models
{
    public class DatosWeather
    {
        public int Dt { get; set; }
        public WeatherMain Main { get; set; } = null!;
        public List<Weather> Weather { get; set; } = null!;
        public WeatherClouds Clouds { get; set; } = null!;
        public WeatherWind Wind { get; set; } = null!;
        public int Visibility { get; set; }
        public decimal Pop { get; set; }
        public WeatherSys Sys { get; set; } = null!;
        public DateTime dt_txt { get; set; }

    }
}
