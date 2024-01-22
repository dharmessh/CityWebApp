namespace CityAPI.Models
{
    public class PlaceDto
    {
        public int PlaceId { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public string? PlaceDescription { get; set; }

    }
}
