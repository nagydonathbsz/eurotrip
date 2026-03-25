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
    public sealed class RoomBookingControllerTest
    {
        public RoomBookingController? _sut;
        private DbContextHelper? _db;

        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            var httpContext = new DefaultHttpContext();
            _sut = new RoomBookingController(_db.CreateDbContext());
            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }
        [TestMethod]
        public async Task GetRoomBookingListOk()
        {
            var result = (await _sut!.GetMyBookingList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<RoomBooking>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db!.RoomBookingList.Contains(u, new RoomBookingComparer()));
            }
        }
        [TestMethod]
        public async Task GetRoomBookingListById_Ok()
        {
            var result = (await _sut!.GetBookingListById(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as RoomBooking;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db!.RoomBookingList.Contains(u, new RoomBookingComparer()));
        }
        [TestMethod]
        public async Task GetRoomBookingListById_NonExistingId()
        {
            var result = (await _sut!.GetBookingListById(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetRoomAvailebleById_Ok()
        {
            var result = (await _sut!.GetBookingListStatus(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            //var u = result.Value as string;
            //Assert.IsNotNull(u);
        }
        [TestMethod]
        public async Task GetRoomAvailebleById_NonExistingId()
        {
            var result = (await _sut!.GetBookingListStatus(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PostRoomBooking_ReturnsCreated()
        {
            var rb = new RoomBooking
            {
                UserId = 3,
                RoomId = 1,
                CheckIn = new DateTime(2026, 2, 28, 5, 0, 0),
                CheckOut = new DateTime(2026, 3, 10, 5, 0, 0),
                Rating = 4
            };
            var result = await _sut!.PostBooking(rb) as CreatedResult;
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public async Task PostRoomBooking_ReturnBadRequest()
        {
            var rb = new RoomBooking
            {
                UserId = 3,
                RoomId = null,
                CheckIn = new DateTime(2026, 2, 28, 5, 0, 0),
                CheckOut = new DateTime(2026, 3, 10, 5, 0, 0),
                Rating = 4
            };
            var result = await _sut!.PostBooking(rb) as CreatedResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task PutRoomBooking_ReturnModified()
        {
            var rb = new RoomBooking
            {
                UserId = 3,
                RoomId = 1,
                CheckIn = new DateTime(2026, 2, 28, 5, 0, 0),
                CheckOut = new DateTime(2026, 3, 10, 5, 0, 0),
                Rating = 4
            };
            var result = await _sut!.PutBooking(1, rb) as OkObjectResult;
            Assert.IsNotNull(result);
            rb = result.Value as RoomBooking;
            Assert.IsNotNull(rb);
        }
        [TestMethod]
        public async Task PutRoomBooking_ReturnNotModified()
        {
            var rb = new RoomBooking
            {
                UserId = 3,
                RoomId = null,
                CheckIn = new DateTime(2026, 2, 28, 5, 0, 0),
                CheckOut = new DateTime(2026, 3, 10, 5, 0, 0),
                Rating = 4
            };
            var result = await _sut!.PutBooking(1, rb) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutRoomBooking_ReturnNotExists()
        {
            var rb = new RoomBooking
            {
                UserId = 3,
                RoomId = 1,
                CheckIn = new DateTime(2026, 2, 28, 5, 0, 0),
                CheckOut = new DateTime(2026, 3, 10, 5, 0, 0),
                Rating = 4
            };
            var result = await _sut!.PutBooking(99, rb) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task DeleteRoomBooking_ReturnDeleted()
        {
            var rb = new RoomBooking
            {
                UserId = 3,
                RoomId = 1,
                CheckIn = new DateTime(2026, 2, 28, 5, 0, 0),
                CheckOut = new DateTime(2026, 3, 10, 5, 0, 0),
                Rating = 4
            };
            var deleted = await _sut!.DeleteBooking(1) as OkObjectResult;
            Assert.IsNotNull(deleted);
            rb = deleted.Value as RoomBooking;
            Assert.IsNotNull(rb);
        }
        [TestMethod]
        public async Task DeleteRoomBooking_ReturnNotExists()
        {
            var deleted = await _sut!.DeleteBooking(99) as OkObjectResult;
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public async Task TestAll_RoomBooking()
        {
            var newBooking = new RoomBooking { UserId = 1, RoomId = 1, CheckIn = DateTime.Now, CheckOut = DateTime.Now.AddDays(1), Rating = 5 };
            var postRes = await _sut!.PostBooking(newBooking) as CreatedResult;
            Assert.IsNotNull(postRes);
            var created = postRes.Value as RoomBooking;
            int id = created!.Id;

            var getRes = await _sut!.GetBookingListById(id) as OkObjectResult;
            Assert.IsNotNull(getRes);

            created.Rating = 4;
            var putRes = await _sut!.PutBooking(id, created) as OkObjectResult;
            Assert.IsNotNull(putRes);

            var delRes = await _sut!.DeleteBooking(id) as OkObjectResult;
            Assert.IsNotNull(delRes);
        }
    }
}
