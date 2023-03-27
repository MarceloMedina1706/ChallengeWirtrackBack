namespace Viajes.Models
{
    public class ResponseWeather
    {
        public string? Cod { get; set; }
        public int Message { get; set; }
        public int? Cnt { get; set; }
        public List<DatosWeather> List { get; set; } = null!;
    }
}
