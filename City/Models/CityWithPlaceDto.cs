namespace CityAPI.Models
{
    public class CityWithPlaceDto
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public string? CityDescription { get; set; }
        public int NumberOfPointsOfInterest
        {
            get
            {
                return Places.Count;
            }
        }

        public ICollection<PlaceDto> Places { get; set; }
            = new List<PlaceDto>();
    }
}
