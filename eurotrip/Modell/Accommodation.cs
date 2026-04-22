using K4os.Compression.LZ4.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace eurotrip.Modell
{
    [Table("accommodation")]
    [PrimaryKey(nameof(Id))]
    public class Accommodation
    {
        [Column("accommodation_id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("image")]
        public string? Image { get; set; }
        [Column("city_id")]
        public int CityId { get; set; }
        [Column("rating")]
        public decimal Rating { get; set; }
        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}
