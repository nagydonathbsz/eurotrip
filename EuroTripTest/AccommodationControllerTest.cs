using eurotrip.Controllers;
using eurotrip.Modell;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EuroTripTest
{
    [TestClass]
    public sealed class AccommodationControllerTest
    {
        public AccommodationController? _sut;
        private DbContextHelper _db;

        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            var httpContext = new DefaultHttpContext();
            _sut = new AccommodationController(_db.CreateDbContext());
            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }
        [TestMethod]
        public async Task GetAccommodationList_OkAccommodationList()
        {
            var result = (await _sut!.GetAccommodationList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<Accommodation>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db.AccommodationList.Contains(u, new AccommodationComparer()));
            }
            Assert.AreEqual(ul.Count(), _db?.AccommodationList.Count());
        }
        [TestMethod]
        public async Task GetAccommodationListById_OkAccommodationListById()
        {
            var result = (await _sut!.GetAccommodationListById(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as Accommodation;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.AccommodationList.Contains(u, new AccommodationComparer()));
        }
        [TestMethod]
        public async Task GetAccommodationListById_NonExistingId()
        {
            var result = (await _sut!.GetAccommodationListById(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetAccommodationListById_InvalidId()
        {
            var result = (await _sut!.GetAccommodationListById(0)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetAccommodationListByCityId_OkAccommodationListByCityId()
        {
            var result = (await _sut!.GetAccommodationListByCityId(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var list = result.Value as IEnumerable<Accommodation>;
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Any());
            foreach (var u in list)
            {
                Assert.IsTrue(_db.AccommodationList.Contains(u, new AccommodationComparer()));
            }
        }
        [TestMethod]
        public async Task GetAccommodationListByCityId_NonExistingId_ShouldReturnNotFound()
        {
            var result = await _sut!.GetAccommodationListByCityId(99);

           
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetAccommodationListByCityId_InvalidCityId_ShouldReturnNotFound()
        {  
            var result = await _sut!.GetAccommodationListByCityId(0);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        [TestMethod]
        public async Task PostAccommodation_ReturnsCreated()
        {
            var acco = new Accommodation
            {
                Name = "Dalmás",
                Address="d",
                Phone="06",
                Image="d.png",
                CityId=3,
            };
            var result = await _sut!.PostAccommodation(acco) as CreatedResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PostAccommodation_ReturnBadRequest() 
        {
            var acco = new Accommodation
            {
                Name = "Dalmás",
                Address = "",
                Phone = "06",
                Image = "d.png",
                CityId = 3,
            };
            var result = await _sut!.PostAccommodation(acco) as CreatedResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task PostAccommodation_ReturnAlreadyExists()
        {
            var acco = new Accommodation
            {
                Name = "AlbertHotel",
                Address = "a",
                Phone = "06",
                Image = "a.png",
                CityId = 1,
            };
            var result = await _sut!.PostAccommodation(acco) as CreatedResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutAccommodation_ReturnModified()
        {
            var acco = new Accommodation
            {
                Name = "Dalmás",
                Address = "d",
                Phone = "06",
                Image = "d.png",
                CityId = 1,
            };
            var result = await _sut!.PutAccommodation(1,acco) as OkObjectResult;
            Assert.IsNotNull(result);
            acco = result.Value as Accommodation;
            Assert.IsNotNull(acco);
        }
        [TestMethod]
        public async Task PutAccommodation_ReturnNotModified()
        {
            var acco = new Accommodation
            {
                Name = "",
                Address = "d",
                Phone = "06",
                Image = "d.png",
                CityId = 1,
            };
            var result = await _sut!.PutAccommodation(1, acco) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutAccommodation_ReturnNotExists()
        {
            var acco = new Accommodation
            {
                Name = "Dalmás",
                Address = "d",
                Phone = "06",
                Image = "d.png",
                CityId = 1,
            };
            var result = await _sut!.PutAccommodation(99, acco) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task DeleteAccommodation_ReturnDeleted()
        {
            var acco = new Accommodation
            {
                Name = "Dalmás",
                Address = "d",
                Phone = "06",
                Image = "d.png",
                CityId = 1,
            };
            var deleted = await _sut!.DeleteAccommodation(1) as OkObjectResult;
            Assert.IsNotNull(deleted);
            acco = deleted.Value as Accommodation;
            Assert.IsNotNull(acco);
        }
        [TestMethod]
        public async Task DeleteAccommodation_ReturnNotExists()
        {
            var deleted = await _sut!.DeleteAccommodation(0) as OkObjectResult;
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public async Task TestAll_Accommodation()
        {
            var newAcco = new Accommodation
            {
                Name = "Test Hotel",
                Address = "Test Street 1.",
                Phone = "123456",
                Image = "test.png",
                CityId = 1
            };

            var postResult = await _sut!.PostAccommodation(newAcco) as CreatedResult;
            Assert.IsNotNull(postResult, "A POST művelet nem sikerült.");
            var createdAcco = postResult.Value as Accommodation;
            Assert.IsNotNull(createdAcco);
            int newId = createdAcco.Id;

            var getResult = await _sut!.GetAccommodationListById(newId) as OkObjectResult;
            Assert.IsNotNull(getResult, "A létrehozott elem nem kérhető le.");
            var fetchedAcco = getResult.Value as Accommodation;
            Assert.AreEqual("Test Hotel", fetchedAcco?.Name);

            fetchedAcco!.Name = "Updated Test Hotel";
            var putResult = await _sut!.PutAccommodation(newId, fetchedAcco) as OkObjectResult;
            Assert.IsNotNull(putResult, "A PUT művelet (módosítás) nem sikerült.");

            var getUpdatedResult = await _sut!.GetAccommodationListById(newId) as OkObjectResult;
            var updatedAcco = getUpdatedResult?.Value as Accommodation;
            Assert.AreEqual("Updated Test Hotel", updatedAcco?.Name, "A név nem frissült az adatbázisban.");

            var deleteResult = await _sut!.DeleteAccommodation(newId) as OkObjectResult;
            Assert.IsNotNull(deleteResult, "A DELETE művelet nem sikerült.");

            var finalGetResult = await _sut!.GetAccommodationListById(newId) as OkObjectResult;
            Assert.IsNull(finalGetResult, "Az elem még mindig létezik a törlés után.");
        }
    }
}
