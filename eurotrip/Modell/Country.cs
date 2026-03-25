using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace eurotrip.Modell
{
    [Table("country")]
    [PrimaryKey(nameof(Id))]
    public class Country
    {
        [Column("country_id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("language")]
        public string? Lang { get; set; }
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
        public ICollection<City>? Cities { get; set; }
    }
}
