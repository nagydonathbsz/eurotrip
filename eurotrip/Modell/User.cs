using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace eurotrip.Modell
{
    [Table("user")]
    [PrimaryKey(nameof(Id))]
    public class User
    {
        [Column("user_id")]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("phone")]
        public string? Phone { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("is_admin")]
        public int isAdmin { get; set; }
        public ICollection<TableReservation>? TRs { get; set; }
        public ICollection<RoomBooking>? RBs { get; set; }
    }
}
