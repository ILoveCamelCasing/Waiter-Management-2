using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Web.Http.Results;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Security;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Service.Controllers;

namespace WaiterManagement.Service.Tests.Controllers
{
  [TestClass]
  public class AccountControllerTests
  {
    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    public void LoginWaiterTest()
    {
      //Arrange
      var waiterGuid = Guid.NewGuid();
      var waiterLogin = "waiterLogin";
      var waiterPassword = "password";
      var waiterFirstHash = HashUtility.CreateFirstHash(waiterPassword, waiterLogin);
      var waiterSecondHash = HashUtility.CreateSecondHash(waiterFirstHash);

      var viewProviderMock = new Mock<IViewProvider>();
      viewProviderMock.Setup(vp => vp.Get<WaiterView>()).Returns(new[] { new WaiterView() { FirstName = "firstName", LastName = "lastName", Login = waiterLogin, WaiterGuid = waiterGuid, WaiterId = 0 } }.AsQueryable());
      viewProviderMock.Setup(vp => vp.Get<UserView>()).Returns(new[] { new UserView() { Id = 0, CommonId = Guid.NewGuid(), UserId = waiterGuid, SecondHash = waiterSecondHash } }.AsQueryable());

      var unitOfWorkMock = new Mock<IUnitOfWork>();
      unitOfWorkMock.Setup(uow => uow.Add(It.IsAny<ActiveUser>())).Returns<ActiveUser>(au => au);

      var accountController = new AccountController(unitOfWorkMock.Object, viewProviderMock.Object);

      //Act
      var actionResult = accountController.LoginWaiter(waiterLogin, waiterFirstHash);

      //Assert
      var okNegotiatedContentResult = actionResult as OkNegotiatedContentResult<Guid>;
      Assert.IsNotNull(okNegotiatedContentResult);
      Assert.IsTrue(okNegotiatedContentResult.Content != Guid.Empty);
    }

    [TestMethod]
    public void LoginTableTest()
    {
      //Arrange
      var tableGuid = Guid.NewGuid();
      var tableLogin = "tableLogin";
      var tablePassword = "password";
      var tableFirstHash = HashUtility.CreateFirstHash(tablePassword, tableLogin);
      var tableSecondHash = HashUtility.CreateSecondHash(tableFirstHash);

      var viewProviderMock = new Mock<IViewProvider>();
      viewProviderMock.Setup(vp => vp.Get<TableView>()).Returns(new[] { new TableView() { Title = "title", Description = "description", Login = tableLogin, TableGuid = tableGuid, TableId = 0 } }.AsQueryable());
      viewProviderMock.Setup(vp => vp.Get<UserView>()).Returns(new[] { new UserView() { Id = 0, CommonId = Guid.NewGuid(), UserId = tableGuid, SecondHash = tableSecondHash } }.AsQueryable());

      var unitOfWorkMock = new Mock<IUnitOfWork>();
      unitOfWorkMock.Setup(uow => uow.Add(It.IsAny<ActiveUser>())).Returns<ActiveUser>(au => au);

      var accountController = new AccountController(unitOfWorkMock.Object, viewProviderMock.Object);

      //Act
      var actionResult = accountController.LoginTable(tableLogin, tableFirstHash);

      //Assert
      var okNegotiatedContentResult = actionResult as OkNegotiatedContentResult<Guid>;
      Assert.IsNotNull(okNegotiatedContentResult);
      Assert.IsTrue(okNegotiatedContentResult.Content != Guid.Empty);
    }

    [TestMethod]
    public void AddWaiterTest()
    {
      //Arrange
      var waiterGuid = Guid.NewGuid();
      var waiterLogin = "waiterLogin";
      var waiterPassword = "password";
      var waiterFirstHash = HashUtility.CreateFirstHash(waiterPassword, waiterLogin);

      var addWaiterCommand = new AddWaiterCommand() { FirstName = "firstName", LastName = "lastName", Login = waiterLogin, FirstHash = waiterFirstHash };

      var viewProviderMock = new Mock<IViewProvider>();
      viewProviderMock.Setup(vp => vp.Get<WaiterView>()).Returns(Enumerable.Empty<WaiterView>().AsQueryable());

      var unitOfWorkMock = new Mock<IUnitOfWork>();
      unitOfWorkMock.Setup(uow => uow.Add(It.IsAny<Waiter>())).Returns<Waiter>(w => w);
      unitOfWorkMock.Setup(uow => uow.Add(It.IsAny<User>())).Returns<User>(u => u);

      var accountController = new AccountController(unitOfWorkMock.Object, viewProviderMock.Object);

      //Act
      var actionResult = accountController.AddWaiter(addWaiterCommand);

      //Assert
      var okNegotiatedContentResult = actionResult as OkNegotiatedContentResult<bool>;
      Assert.IsNotNull(okNegotiatedContentResult);
      Assert.IsTrue(okNegotiatedContentResult.Content);
    }

    [TestMethod]
    public void AddTableTest()
    {
      //Arrange
      var tableGuid = Guid.NewGuid();
      var tableLogin = "tableLogin";
      var tablePassword = "password";
      var tableFirstHash = HashUtility.CreateFirstHash(tablePassword, tableLogin);

      var addtableCommand = new AddTableCommand() { Title = "title", Description = "description", Login = tableLogin, FirstHash = tableFirstHash };

      var viewProviderMock = new Mock<IViewProvider>();
      viewProviderMock.Setup(vp => vp.Get<TableView>()).Returns(Enumerable.Empty<TableView>().AsQueryable());

      var unitOfWorkMock = new Mock<IUnitOfWork>();
      unitOfWorkMock.Setup(uow => uow.Add(It.IsAny<Table>())).Returns<Table>(w => w);
      unitOfWorkMock.Setup(uow => uow.Add(It.IsAny<User>())).Returns<User>(u => u);

      var accountController = new AccountController(unitOfWorkMock.Object, viewProviderMock.Object);

      //Act
      var actionResult = accountController.AddTable(addtableCommand);

      //Assert
      var okNegotiatedContentResult = actionResult as OkNegotiatedContentResult<bool>;
      Assert.IsNotNull(okNegotiatedContentResult);
      Assert.IsTrue(okNegotiatedContentResult.Content);
    }
  }
}
