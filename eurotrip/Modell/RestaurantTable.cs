using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace eurotrip.Modell
{
    [Table("restauranttable")]
    [PrimaryKey(nameof(Id))]
    public class RestaurantTable
    {
        [Column("table_id")]
        public int Id { get; set; }
        [Column("restaurant_id")]
        public int? RestaurantId { get; set; }
        [Column("seats")]
        public int Seats { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        public Restaurant? Restaurant { get; set; }
        public ICollection<TableReservation>? TRs { get; set; }
    }
}
