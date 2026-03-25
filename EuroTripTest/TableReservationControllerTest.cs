using eurotrip.Controllers;
using eurotrip.Modell;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace EuroTripTest
{
    [TestClass]
    public sealed class TableReservationControllerTest
    {
        public TableReservationController? _sut;
        private DbContextHelper _db;
        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            _sut = new TableReservationController(_db.CreateDbContext());
        }
        [TestMethod]
        public async Task GetTableReservationList_Ok()
        {
            var result = (await _sut!.GetMyTableReservationList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<TableReservation>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db.TableReservationList.Contains(u, new TableReservationComparer()));
            }
        }
        [TestMethod]
        public async Task GetTableReservationById_IsAvailable()
        {
            var result = (await _sut!.GetTableReservationById_IsAvailable(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            //var u = result.Value as string;
            //Assert.IsNotNull(u);
        }
        [TestMethod]
        public async Task GetTableReservationById_NonExistingId()
        {
            var result = (await _sut!.GetTableReservationById_IsAvailable(99)) as OkObjectResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestAll_TableReservation()
        {
            var listResult = await _sut!.GetMyTableReservationList() as OkObjectResult;
            Assert.IsNotNull(listResult);
            var reservations = listResult.Value as IEnumerable<TableReservation>;

            // var getById = await _sut!.GetTableReservationListById(1) as OkObjectResult;
            // Assert.IsNotNull(getById);
        }
    }
}
