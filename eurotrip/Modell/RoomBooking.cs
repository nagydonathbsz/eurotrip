using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace eurotrip.Modell
{
    [Table("roombooking")]
    [PrimaryKey(nameof(Id))]
    public class RoomBooking
    {
        [Column("booking_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("user_id")]
        public int? UserId { get; set; }
        [Column("room_id")]
        public int? RoomId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }

        [Column("check_in")]
        public DateTime? CheckIn { get; set; }
        [Column("check_out")]
        public DateTime? CheckOut { get; set; }
        [Column("rating")]
        public int? Rating { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("status")]
        public string? Status { get; set; }
    }
}
