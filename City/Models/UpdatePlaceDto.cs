namespace CityAPI.Models
{
    public class UpdatePlaceDto
    {
        public string PlaceName { get; set; } = string.Empty;
        public string? PlaceDescription { get; set; }
    }
}
