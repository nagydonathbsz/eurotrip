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
    public sealed class RoomControllerTest
    {
        public RoomController? _sut;
        private DbContextHelper _db;
        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            var httpContext = new DefaultHttpContext();
            _sut = new RoomController(_db.CreateDbContext());
            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }
        [TestMethod]
        public async Task GetRoomListOkRoomList()
        {
            var result = (await _sut!.GetRoomList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<Room>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db.RoomList.Contains(u, new RoomComparer()));
            }
            Assert.AreEqual(ul.Count(), _db.RoomList.Count());
        }
        [TestMethod]
        public async Task GetRoomListById_OkRoomListById()
        {
            var result = (await _sut!.GetRoomListById(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as Room;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.RoomList.Contains(u, new RoomComparer()));
        }
        [TestMethod]
        public async Task GetRoomListById_NonExistingId()
        {
            var result = (await _sut!.GetRoomListById(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PostRoom_ReturnsCreated()
        {
            var room = new Room
            {
                AccommodationId = 1,
            };
            var result = await _sut!.PostRoom(room) as CreatedResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task PostRoom_ReturnBadRequest()
        {
            var room = new Room
            {
                AccommodationId = null,
            };
            var result = await _sut!.PostRoom(room) as CreatedResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutRoom_ReturnModified()
        {
            var room = new Room
            {
                AccommodationId = 1,
            };
            var result = await _sut!.PutRoom(1, room) as OkObjectResult;
            Assert.IsNotNull(result);
            room = result.Value as Room;
            Assert.IsNotNull(room);
        }
        [TestMethod]
        public async Task PutRoom_ReturnNotModified()
        {
            var room = new Room
            {
                AccommodationId = null,
            };
            var result = await _sut!.PutRoom(1, room) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task PutRoom_ReturnNotExists()
        {
            var room = new Room
            {
                AccommodationId = 1,
            };
            var result = await _sut!.PutRoom(99, room) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task DeleteRoom_ReturnDeleted()
        {
            var room = new Room
            {
                AccommodationId = 1,
            };
            var deleted = await _sut!.DeleteRoom(1) as OkObjectResult;
            Assert.IsNotNull(deleted);
            room = deleted.Value as Room;
            Assert.IsNotNull(room);
        }
        [TestMethod]
        public async Task DeleteRoom_ReturnNotExists()
        {
            var deleted = await _sut!.DeleteRoom(0) as OkObjectResult;
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public async Task TestAll_Room()
        {
            var newRoom = new Room
            {
                AccommodationId = 1
            };

            var postResult = await _sut!.PostRoom(newRoom) as CreatedResult;
            Assert.IsNotNull(postResult, "A POST művelet sikertelen.");

            var createdRoom = postResult.Value as Room;
            Assert.IsNotNull(createdRoom);
            int newId = createdRoom.Id;

            var getResult = await _sut!.GetRoomListById(newId) as OkObjectResult;
            Assert.IsNotNull(getResult, "A létrehozott szoba nem található.");

            createdRoom.AccommodationId = 2;
            var putResult = await _sut!.PutRoom(newId, createdRoom) as OkObjectResult;
            Assert.IsNotNull(putResult, "A PUT (módosítás) sikertelen.");

            var updatedRoom = putResult.Value as Room;
            Assert.AreEqual(2, updatedRoom?.AccommodationId);

            var deleteResult = await _sut!.DeleteRoom(newId) as OkObjectResult;
            Assert.IsNotNull(deleteResult, "A törlés sikertelen.");

            var finalCheck = await _sut!.GetRoomListById(newId) as OkObjectResult;
            Assert.IsNull(finalCheck, "A szoba még mindig létezik a törlés után.");
        }
    }
}
