using eurotrip.Controllers;
using eurotrip.Modell;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace EuroTripTest
{
    [TestClass]
    public sealed class RestaurantControllerTest
    {
        public RestaurantController? _sut;
        private DbContextHelper _db;
        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            var httpContext = new DefaultHttpContext();
            _sut = new RestaurantController(_db.CreateDbContext());
            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }
        [TestMethod]
        public async Task GetRestaurantListOkRestaurantList()
        {
            var result = (await _sut!.GetRestaurantList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<Restaurant>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db.RestaurantList.Contains(u, new RestaurantComparer()));
            }
            Assert.AreEqual(ul.Count(), _db.RestaurantList.Count());
        }
        [TestMethod]
        public async Task GetRestaurantListById_OkRestaurantListById()
        {
            var result =(await _sut!.GetRestaurantListById(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as Restaurant;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.RestaurantList.Contains(u, new RestaurantComparer()));
        }
        [TestMethod]
        public async Task GetRestaurantListById_NonExistingId()
        {
            var result = (await _sut!.GetRestaurantListById(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetRestaurantListByCityId_OkRestaurantListCityById()
        {
            var actionResult = await _sut!.GetRestaurantListByCityId(1);
            var result = actionResult as OkObjectResult;
            Assert.IsNotNull(result, "Az eredmény nem OkObjectResult");
            var list = result.Value as IEnumerable<Restaurant>;
            Assert.IsNotNull(list, "A visszakapott érték nem étterem lista");
            Assert.IsTrue(list.Any(), "A lista üres, pedig kellene benne lennie étteremnek");
            var firstRestaurant = list.First();
            Assert.IsTrue(_db.RestaurantList.Any(r => new RestaurantComparer().Equals(r, firstRestaurant)));
        }
        [TestMethod]
        public async Task GetRestaurantListByCityId_NonExistingId()
        {
            var result = (await _sut!.GetRestaurantListByCityId(99)) as OkObjectResult;
            Assert.IsNotNull(result);
            var list = result.Value as IEnumerable<Restaurant>;
            Assert.IsNotNull(list);
            Assert.AreEqual(0, list.Count());
        }
        [TestMethod]
        public async Task PostRestaurant_ReturnsCreated()
        {
            var rest = new Restaurant
            {
                Name = "DalmasRest",
                Address = "d",
                Image = "d.png",
                CityId = 3,
            };
            var result = await _sut!.PostRestaurant(rest) as CreatedResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PostRestaurant_ReturnBadRequest()
        {
            var rest = new Restaurant
            {
                Name = "",
                Address = "d",
                Image = "d.png",
                CityId = 1,
            };
            var result = await _sut!.PostRestaurant(rest) as CreatedResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task PostRestaurant_ReturnAlreadyExists()
        {
            var rest = new Restaurant
            {
                Name = "AjkaRest",
                Address = "a",
                Image = "a.png",
                CityId = 1,
            };
            var result = await _sut!.PostRestaurant(rest) as CreatedResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutRestaurant_ReturnModified()
        {
            var rest = new Restaurant
            {
                Name = "DalmásRest",
                Address = "d",
                Image = "d.png",
                CityId = 1,
            };
            var result = await _sut!.PutRestaurant(1, rest) as OkObjectResult;
            Assert.IsNotNull(result);
            rest = result.Value as Restaurant;
            Assert.IsNotNull(rest);
        }
        [TestMethod]
        public async Task PutRestaurant_ReturnNotModified()
        {
            var rest = new Restaurant
            {
                Name = "",
                Address = "d",
                Image = "d.png",
                CityId = 1,
            };
            var result = await _sut!.PutRestaurant(1, rest) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutRestaurant_ReturnNotExists()
        {
            var rest = new Restaurant
            {
                Name = "DalmásRest",
                Address = "d",
                Image = "d.png",
                CityId = 1,
            };
            var result = await _sut!.PutRestaurant(99, rest) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task DeleteRestaurant_ReturnDeleted()
        {
            {
                var rest = new Restaurant
                {
                    Name = "DalmásRest",
                    Address = "d",
                    Image = "d.png",
                    CityId = 1,
                };
                var result = await _sut!.DeleteRestaurant(1) as OkObjectResult;
                Assert.IsNotNull(result);
                rest = result.Value as Restaurant;
                Assert.IsNotNull(rest);
            }
        }
        [TestMethod]
        public async Task DeleteRestaurant_ReturnNotExists()
        {
            var result = await _sut!.DeleteRestaurant(0) as OkObjectResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestAll_Restaurant()
        {
            var newRest = new Restaurant
            {
                Name = "Test Rest",
                Address = "Test St 10",
                Image = "test.png",
                CityId = 1
            };
            var postRes = await _sut!.PostRestaurant(newRest) as CreatedResult;
            Assert.IsNotNull(postRes, "A POST művelet sikertelen.");
            var created = postRes.Value as Restaurant;
            int id = created!.Id;

            var getById = await _sut!.GetRestaurantListById(id) as OkObjectResult;
            Assert.IsNotNull(getById, "A létrehozott étterem nem található ID alapján.");

            var getByCity = await _sut!.GetRestaurantListByCityId(1) as OkObjectResult;
            Assert.IsNotNull(getByCity, "A város alapú lekérdezés sikertelen.");

            created.Name = "Updated Test Rest Name";
            var putRes = await _sut!.PutRestaurant(id, created) as OkObjectResult;
            Assert.IsNotNull(putRes, "A módosítás (PUT) nem sikerült.");

            var delRes = await _sut!.DeleteRestaurant(id) as OkObjectResult;
            Assert.IsNotNull(delRes, "A törlés sikertelen.");

            var finalCheck = await _sut!.GetRestaurantListById(id) as OkObjectResult;
            Assert.IsNull(finalCheck, "A törölt elem még mindig lekérdezhető.");
        }
    }
}
