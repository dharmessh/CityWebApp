using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityAPI.Entities
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlaceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PlaceName { get; set; }
        [MaxLength(200)]
        public string? PlaceDescription { get; set; }    

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }

        public Place(string placeName)
        {
            PlaceName = placeName;
        }

    }
}