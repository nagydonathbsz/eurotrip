using eurotrip.Controllers;
using eurotrip.Modell;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace EuroTripTest
{
    [TestClass]
    public sealed class RestaurantTableControllerTest
    {
        public RestaurantTableController? _sut;
        private DbContextHelper _db;

        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            var httpContext = new DefaultHttpContext();
            _sut = new RestaurantTableController(_db.CreateDbContext());
            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }
        [TestMethod]
        public async Task GetRestaurantTableById_Ok()
        {
            var result = (await _sut!.GetTableListByRestaurantId(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as RestaurantTable;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.RestaurantTableList.Contains(u, new RestaurantTableComparer()));
        }
        [TestMethod]
        public async Task GetRestaurantTableListById_NonExist()
        {
            var result = (await _sut!.GetTableListByRestaurantId(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PostRestaurantTable_ReturnsCreated()
        {
            var rt = new RestaurantTable
            {
                RestaurantId = 1,
            };
            var result = await _sut!.PostTable(rt) as CreatedResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task PostRestaurantTable_ReturnBadRequest()
        {
            var rt = new RestaurantTable
            {
                Id = 1,
                RestaurantId = null,
            };
            var result = await _sut!.PostTable(rt) as CreatedResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task PutRestaurantTable_ReturnModified()
        {
            var rt = new RestaurantTable
            {
                RestaurantId = 3,
            };
            var result = await _sut!.PutTable(1, rt) as OkObjectResult;
            Assert.IsNotNull(result);
            rt = result.Value as RestaurantTable;
            Assert.IsNotNull(rt);
        }
        [TestMethod]
        public async Task PutRestaurantTable_ReturnNotModified()
        {
            var rt = new RestaurantTable
            {
                RestaurantId = null,
            };
            var result = await _sut!.PutTable(1, rt) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutRestaurantTable_ReturnNotExists()
        {
            var rt = new RestaurantTable
            {
                Id = 1,
                RestaurantId = 4,
            };
            var result = await _sut!.PutTable(99, rt) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task DeleteRestaurantTable_ReturnDeleted()
        {
            var rt = new RestaurantTable
            {
                Id = 1,
                RestaurantId = 4,
            };
            var deleted = await _sut!.DeleteTable(1) as OkObjectResult;
            Assert.IsNotNull(deleted);
            rt = deleted.Value as RestaurantTable;
            Assert.IsNotNull(rt);
        }
        [TestMethod]
        public async Task DeleteRestaurantTable_ReturnNotExists()
        {
            var deleted = await _sut!.DeleteTable(99) as OkObjectResult;
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public async Task TestAll_RestaurantTable()
        {
            var newTable = new RestaurantTable
            {
                RestaurantId = 1 
            };

            var postResult = await _sut!.PostTable(newTable) as CreatedResult;
            Assert.IsNotNull(postResult, "A POST művelet sikertelen volt.");

            var createdTable = postResult.Value as RestaurantTable;
            Assert.IsNotNull(createdTable);
            int newId = createdTable.Id;

            var getResult = await _sut!.GetTableListByRestaurantId(1) as OkObjectResult;
            Assert.IsNotNull(getResult, "A lekérdezés nem tért vissza eredménnyel.");

            createdTable.RestaurantId = 2;
            var putResult = await _sut!.PutTable(newId, createdTable) as OkObjectResult;
            Assert.IsNotNull(putResult, "A PUT (módosítás) sikertelen volt.");

            var deleteResult = await _sut!.DeleteTable(newId) as OkObjectResult;
            Assert.IsNotNull(deleteResult, "A törlés sikertelen volt.");

            var finalResult = await _sut!.GetTableListByRestaurantId(2) as OkObjectResult;
            if (finalResult?.Value is IEnumerable<RestaurantTable> list)
            {
                Assert.IsFalse(list.Any(x => x.Id == newId), "Az asztal még mindig szerepel a listában törlés után.");
            }
        }
    }
}
