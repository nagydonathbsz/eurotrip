using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace eurotrip.Modell
{
    [Table("city")]
    [PrimaryKey(nameof(Id))]
    public class City
    {
        [Column("city_id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("zip_code")]
        public int ZipCode { get; set; }
        [Column("has_airport")]
        public int HasAirport { get; set; }
        [Column("country_id")]
        public int CountryId { get; set; }
        [ForeignKey(nameof(CountryId))]
        public Country? Country { get; set; }
        public ICollection<Accommodation>? Accommodations { get; set; }
        public ICollection<Restaurant>? Restaurants { get; set; }
    }
}
