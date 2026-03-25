using Microsoft.EntityFrameworkCore;

namespace eurotrip.Modell
{
    public class EuroContext : DbContext
    {
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<TableReservation> TableReservations { get; set; }
        public DbSet<User> Users { get; set; }
        public EuroContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
