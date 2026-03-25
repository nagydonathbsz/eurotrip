using eurotrip.Controllers;
using eurotrip.Modell;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace EuroTripTest
{
    [TestClass]
    public sealed class CountryControllerTest
    {
        public CountryController? _sut;
        private DbContextHelper _db;
        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            _sut = new CountryController(_db.CreateDbContext());
        }
        [TestMethod]
        public async Task GetCountryListOkCountryList()
        {
            var result = (await _sut!.GetCountryList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<Country>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db.CountryList.Contains(u, new CountryComparer()));
            }
            Assert.AreEqual(ul.Count(), _db.CountryList.Count());
        }
        [TestMethod]
        public async Task GetCountryListById_OkCountryListById()
        {
            var result =(await _sut!.GetCountryListById(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as Country;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.CountryList.Contains(u, new CountryComparer()));
        }
        [TestMethod]
        public async Task GetCountryListById_NonExistingId()
        {
            var result = (await _sut!.GetCountryListById(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetCountryListById_InvalidId()
        {
            var result = (await _sut!.GetCountryListById(0)) as OkObjectResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestAll_Country()
        {
            var listResult = await _sut!.GetCountryList() as OkObjectResult;
            Assert.IsNotNull(listResult);
            var countries = listResult.Value as IEnumerable<Country>;
            Assert.AreEqual(_db.CountryList.Count(), countries!.Count());

            var getById = await _sut!.GetCountryListById(1) as OkObjectResult;
            Assert.IsNotNull(getById, "Az ország nem található ID alapján.");
            var country = getById.Value as Country;
            Assert.AreEqual("Hungary", country?.Name);
        }
    }
}
