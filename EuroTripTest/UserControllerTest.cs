using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using eurotrip.Controllers;
using eurotrip.Modell;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace EuroTripTest
{
    [TestClass]
    public sealed class UserControllerTest
    {
        public UserController? _sut;
        private DbContextHelper _db;
        
        [TestInitialize]
        public void Initalize()
        {
            _db = new DbContextHelper();
            _sut = new UserController(_db.CreateDbContext());
        }
        [TestMethod]
        public async Task GetUserListOkUserList()
        {
            var result = (await _sut!.GetUserList()) as OkObjectResult;
            Assert.IsNotNull(result);
            var ul = result.Value as IEnumerable<User>;
            Assert.IsNotNull(ul);
            foreach (var u in ul)
            {
                Assert.IsTrue(_db.UserList.Contains(u, new UserComparer()));
            }
            Assert.AreEqual(ul.Count(), _db.UserList.Count());
        }
        [TestMethod]
        public async Task GetUserListById_OkUserListById()
        {
            var result =(await _sut!.GetUserListById(1)) as OkObjectResult;
            Assert.IsNotNull(result);
            var u = result.Value as User;
            Assert.IsNotNull(u);
            Assert.IsTrue(_db.UserList.Contains(u, new UserComparer()));
        }
        [TestMethod]
        public async Task GetUserListById_NonExistingId()
        {
            var result = (await _sut!.GetUserListById(99)) as OkObjectResult;
            Assert.IsNull(result);
        }
        [TestMethod]
        public async Task GetUserListById_InvalidId()
        {
            var result = (await _sut!.GetUserListById(0)) as OkObjectResult;
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task TestAll_User()
        {
            var listResult = await _sut!.GetUserList() as OkObjectResult;
            Assert.IsNotNull(listResult);
            var users = listResult.Value as IEnumerable<User>;
            Assert.AreEqual(_db.UserList.Count(), users!.Count());

            var getById = await _sut!.GetUserListById(1) as OkObjectResult;
            Assert.IsNotNull(getById);
            var user = getById.Value as User;
            Assert.AreEqual("Albert", user?.Name);
        }
    }
}
