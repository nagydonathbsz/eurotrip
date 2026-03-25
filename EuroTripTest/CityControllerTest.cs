using eurotrip.Controllers;
using eurotrip.Modell;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace EuroTripTest
{
    [TestClass]
    public sealed class CityControllerTest
    {
        public CityController? _sut;
        private DbContextHelper _db;
        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            _sut = new CityController(_db.CreateDbContext());
        }
        [TestMethod]
        public async Task GetCityListOkCityList()
        {
            var result = (await _sut!.GetCityList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<City>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db.CityList.Contains(u, new CityComparer()));
            }
            Assert.AreEqual(ul.Count(), _db.CityList.Count());
        }
        [TestMethod]
        public async Task GetCityListById_OkCityListById()
        {
            var result =(await _sut!.GetCityListById(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as City;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.CityList.Contains(u, new CityComparer()));
        }
        [TestMethod]
        public async Task GetCityListById_NonExistingId()
        {
            var result = (await _sut!.GetCityListById(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetCityListById_InvalidId()
        {
            var result = (await _sut!.GetCityListById(0)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetCityListById_OkCityListByCountryId()
        {
            var result = (await _sut!.GetCityListByCountryId(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as City;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.CityList.Contains(u, new CityComparer()));
        }
        [TestMethod]
        public async Task GetCityListByCountryId_NonExistingId()
        {
            var result = (await _sut!.GetCityListByCountryId(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetCityListByCountryId_InvalidId()
        {
            var result = (await _sut!.GetCityListByCountryId(0)) as OkObjectResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestAll_City()
        {
            var listResult = await _sut!.GetCityList() as OkObjectResult;
            Assert.IsNotNull(listResult);
            var cities = listResult.Value as IEnumerable<City>;
            Assert.AreEqual(_db.CityList.Count(), cities!.Count(), "A listák hossza nem egyezik.");

            var getById = await _sut!.GetCityListById(1) as OkObjectResult;
            Assert.IsNotNull(getById, "Az 1-es ID-jú város nem található.");

            var getByCountry = await _sut!.GetCityListByCountryId(1) as OkObjectResult;
            Assert.IsNotNull(getByCountry, "Az ország alapú lekérdezés nem tért vissza eredménnyel.");
        }
    }
}
