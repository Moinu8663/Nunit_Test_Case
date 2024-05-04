using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using User_for__NUnit_Test.Controllers;
using User_for__NUnit_Test.Model;
using User_for__NUnit_Test.Repository;
using User_for__NUnit_Test.Service;

namespace Nunit_test_for_user
{
    public class Tests
    {

        private DbContextOptions<UserContext> dbContextOptions;
        private Mock<UserContext> mockcontext;
        private Repo repo;
        private Mock<Irepo> mockrepo;
        private service services;
        private Mock<Iservice> mockservice;
        private UserController usercontroller;
        [SetUp]
        public void Setup()
        {
            // Set up mock ContectContext using an in-memory database
            dbContextOptions = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            mockcontext = new Mock<UserContext>(dbContextOptions);

            repo = new Repo(mockcontext.Object);
            mockrepo = new Mock<Irepo>();

            services = new service(mockrepo.Object);
            mockservice = new Mock<Iservice>();

            usercontroller = new UserController(mockservice.Object);
        }

        // test cases for controller
        [Test]
        public void Post_ReturnsOkResult()
        {
            // Arrange
            User user = new User { Id = 1,Name = "moinu",Age = 26,Mobile_No = "1234567890",Email ="md@gmail.com" };

            // Act
            IActionResult result = usercontroller.Post(user);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual("New User added successfully.", objectResult.Value);
            mockservice.Verify(s => s.Add(It.IsAny<User>()), Times.Once);
        }
        [Test]
        public void Get_ReturnsOkResult()
        {
            // Arrange
            mockservice.Setup(service => service.GetAll()).Returns(new List<User>());

            // Act
            IActionResult result = usercontroller.Get();

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var ObjectResult = result as ObjectResult;
            Assert.AreEqual(200, ObjectResult.StatusCode);
        }
        [Test]
        public void GetByMobileNo_ReturnsOkResult()
        {
            // Arrange
            string mobileNo = "1234567890";
            mockservice.Setup(service => service.GetUserByMobileNo(mobileNo)).Returns(new User());

            // Act
            IActionResult result = usercontroller.Get(mobileNo);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var ObjectResult = result as ObjectResult;
            Assert.AreEqual(200, ObjectResult.StatusCode);
        }
        [Test]
        public void Put_ReturnsOkResult()
        {
            // Arrange
            string mobileNo = "1234567890";
            User user = new User { Mobile_No = mobileNo };

            // Act
            IActionResult result = usercontroller.Put(user, mobileNo);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var ObjectResult = result as ObjectResult;
            Assert.AreEqual(200, ObjectResult.StatusCode);
        }
        [Test]
        public void Delete_ReturnsOkResult()
        {
            // Arrange
            string mobileNo = "1234567890";

            // Act
            IActionResult result = usercontroller.Delete(mobileNo);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var ObjectResult = result as ObjectResult;
            Assert.AreEqual(200, ObjectResult.StatusCode);
        }
    }
}