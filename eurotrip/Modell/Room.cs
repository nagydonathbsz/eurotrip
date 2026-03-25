using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace eurotrip.Modell
{
    [Table("room")]
    [PrimaryKey(nameof(Id))]
    public class Room
    {
        [Column("room_id")]
        public int Id { get; set; }
        [Column("accommodation_id")]
        public int? AccommodationId { get; set; }
        [Column("room_number")]
        public string? RoomNumber { get; set; }
        [Column("capacity")]
        public int Capacity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [ForeignKey(nameof(AccommodationId))]
        public Accommodation? Accommodation { get; set; }
        public ICollection<RoomBooking>? RBs { get; set; }
    }
}
