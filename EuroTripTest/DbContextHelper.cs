using eurotrip.Modell;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace EuroTripTest
{
    internal class DbContextHelper
    {
        public List<User> UserList = [
                 new User { Id = 1, Name = "Albert", Email = "a", Phone="06", Password="al", isAdmin=0, Token="hgkjgdguj"},
                 new User { Id = 2, Name = "Bertha", Email = "b", Phone="06", Password="be", isAdmin=0 },
                 new User { Id = 3, Name = "Ceasar", Email = "c", Phone="06", Password="ce", isAdmin=0 }
            ];
        public List<Accommodation> AccommodationList = [
                 new Accommodation { Id = 1, Name = "AlbertHotel", Address = "a", Image="a.png", Phone="06", CityId=1 },
                 new Accommodation { Id = 2, Name = "BellaHotel", Address = "b", Image="b.png", Phone="06", CityId=2 },
                 new Accommodation { Id = 3, Name = "CeasarHotel", Address = "c", Image="c.png", Phone="06", CityId=3 }
            ];
        public List<City> CityList = [
                 new City { Id = 1, Name = "Ajka", HasAirport=1, ZipCode=10234, CountryId=1 },
                 new City { Id = 2, Name = "Buda", HasAirport=1, ZipCode=10235, CountryId=2 },
                 new City { Id = 3, Name = "Celdolk", HasAirport=0, ZipCode=10236, CountryId=3}
            ];
        public List<Country> CountryList = [
                 new Country { Id = 1, Name = "Hungary", PhoneNumber="0630", Lang="Hungarian" },
                 new Country { Id = 2, Name = "Hungary", PhoneNumber = "0630", Lang = "Hungarian" },
                 new Country { Id = 3, Name = "Ciprus", PhoneNumber = "0730", Lang = "Turkish"}
            ];
        public List<Restaurant> RestaurantList = [
                 new Restaurant{ Id = 1, Name = "AjkaRest",  Address = "a", Image="a.png", Phone="06", CityId=1 },
                 new Restaurant{ Id = 2, Name = "BudaRest",  Address = "b", Image="b.png", Phone="06", CityId=2 },
                 new Restaurant{ Id = 3, Name = "CeldolkRest",  Address = "c", Image="c.png", Phone="06", CityId=3 },
            ];
        public List<RestaurantTable> RestaurantTableList = [
                 new RestaurantTable { Id = 1, RestaurantId=1 },
                 new RestaurantTable { Id = 2, RestaurantId=2 },
                 new RestaurantTable { Id = 3, RestaurantId=3 }
            ];
        public List<RoomBooking> RoomBookingList = [
                 new RoomBooking { Id = 1, UserId = 1, RoomId=1, CheckIn=new DateTime(2026,2,28,5,0,0), CheckOut=new DateTime(2026,3,10,5,0,0), Rating=4 },
                 new RoomBooking { Id = 2,  UserId = 2, RoomId=2, CheckIn=new DateTime(2026,3,30,16,0,0), CheckOut=new DateTime(2026,4,10,16,0,0), Rating=5},
                 new RoomBooking { Id = 3,  UserId = 3, RoomId=3, CheckIn=new DateTime(2026,3,1,10,0,0), CheckOut=new DateTime(2026,3,10,10,0,0), Rating=null}
            ];
        public List<Room> RoomList = [
                 new Room { Id = 1, AccommodationId=1 },
                 new Room { Id = 2, AccommodationId=2 },
                 new Room { Id = 3, AccommodationId=3 }
            ];
        public List<TableReservation> TableReservationList = [
                 new TableReservation { Id = 1, UserId = 1, TableId=1,  ResTime=new DateTime(2026,2,28,5,0,0), ResDate=new DateTime(2026,3,1,15,0,0) },
                 new TableReservation { Id = 2,  UserId = 2, TableId=2,  ResTime=new DateTime(2026,2,21,12,0,0), ResDate=new DateTime(2026,2,22,15,30,0)},
                 new TableReservation { Id = 3,  UserId = 3, TableId=3,  ResTime=new DateTime(2026,2,25,10,0,0), ResDate=new DateTime(2026,2,28,10,30,0)}
            ];
        public EuroContext CreateDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<EuroContext>()
                .UseSqlite(connection)
                .Options;

            var context = new EuroContext(options);
            context.Database.EnsureCreated();
            context.Users.AddRange(UserList);
            context.Accommodations.AddRange(AccommodationList);
            context.Cities.AddRange(CityList);
            context.Countries.AddRange(CountryList);
            context.Restaurants.AddRange(RestaurantList);
            context.RestaurantTables.AddRange(RestaurantTableList);
            context.RoomBookings.AddRange(RoomBookingList);
            context.Rooms.AddRange(RoomList);
            context.TableReservations.AddRange(TableReservationList);
            context.SaveChanges();
            return context;
        }
    }
}
