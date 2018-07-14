using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CallCenter.BLL.Core;
using Moq;
using System.Threading.Tasks;
using CallCenter.API.Controllers;
using AutoMapper;

namespace CallCenter.API.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private static Mock<ICryptoService> mockCryptoService;
        private static Mock<IMapper> mockMapper;
        private static Mock<IUserService> mockUserService;

        [TestInitialize]
        public void Initialize()
        {
            mockCryptoService = new Mock<ICryptoService>();
            mockMapper = new Mock<IMapper>();
            mockUserService = new Mock<IUserService>();
        }

        [TestMethod]
        public void IsUserExist_should_return_false()
        {
            //arrange
            string username = "user1";
            mockUserService.Setup(u => u.IsUserExist(username))
                .Returns(Task.FromResult(false));
            var userController = new UserController(mockUserService.Object, mockCryptoService.Object, mockMapper.Object);
            //act
            var result = userController.IsUserExist(username).Result;
            //assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetGlobalUserInfo_should_return_not_null()
        {
            //arrange
            mockUserService.Setup(u => u.GetUsersCount())
                .Returns(Task.FromResult(6));
            mockUserService.Setup(u => u.GetAdminsCount())
                .Returns(Task.FromResult(78));
            mockUserService.Setup(u => u.GetManagersCount())
                .Returns(Task.FromResult(11));
            var userController = new UserController(mockUserService.Object, mockCryptoService.Object, mockMapper.Object);
            //act
            var result = userController.GetGlobalUserInfo().Result;
            //assert
            Assert.IsNotNull(result);
        }
    }
}