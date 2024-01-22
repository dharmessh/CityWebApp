namespace CityAPI.Models
{
    public class CityDto
    {
        public int Id { get; set; }
       
        public string CityName { get; set; } = String.Empty;

        public string? CityDescription { get; set; }
        
    }
}
