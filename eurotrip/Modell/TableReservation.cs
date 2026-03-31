using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations.Schema;

namespace eurotrip.Modell
{
    [Table("tablereservation")]
    [PrimaryKey(nameof(Id))]
    public class TableReservation
    {
        [Column("reservation_id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("table_id")]
        public int TableId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        [ForeignKey(nameof(TableId))]
        public RestaurantTable? Table { get; set; }

        [Column("reservation_start")]
        public DateTime ReservationStart { get; set; }
        [Column("reservation_end")]
        public DateTime ReservationEnd { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("status")]
        public string? Status { get; set; }
    }
}
