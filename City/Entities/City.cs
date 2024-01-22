using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityAPI.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }

        [MaxLength(200)]
        public string? CityDescription { get; set; }

        public List<Place> Places { get; set; } = new List<Place>();

        public City(string cityName)
        {
            CityName = cityName;
        }
    }
}
