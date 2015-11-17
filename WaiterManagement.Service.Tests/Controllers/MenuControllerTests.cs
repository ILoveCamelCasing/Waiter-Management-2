using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using WaiterManagement.Common.Views;
using WaiterManagement.Common.Views.Abstract;
using WaiterManagement.Service.Controllers;

namespace WaiterManagement.Service.Tests.Controllers
{
  [TestClass]
  public class MenuControllerTests
  {
    [TestMethod]
    public void GetMenuTest()
    {
      //Arrange

      var menu = new[]
      {
        new MenuItemView()
        {
          CategoryId = 1,
          CategoryTitle = "Food",
          MenuItemId = 1,
          MenuItemGuid = Guid.NewGuid(),
          Title = "Burger",
          Description = "Delicious burger"
        },
        new MenuItemView()
        {
          CategoryId = 1,
          CategoryTitle = "Food",
          MenuItemId = 2,
          MenuItemGuid = Guid.NewGuid(),
          Title = "French fries",
          Description = "Not so delicious french fries"
        },
        new MenuItemView()
        {
          CategoryId = 2,
          CategoryTitle = "Drinks",
          MenuItemId = 3,
          MenuItemGuid = Guid.NewGuid(),
          Title = "Grape soda",
          Description = "Plain grape soda"
        }
      };

      var viewProviderMock = new Mock<IViewProvider>();
      viewProviderMock.Setup(vp => vp.Get<MenuItemView>()).Returns(menu.AsQueryable());

      var menuController = new MenuController(viewProviderMock.Object);

      //Act
      var actionResult = menuController.GetMenu();

      //Assert
      var okNegotiatedContentResult = actionResult as OkNegotiatedContentResult<IEnumerable<MenuItemView>>;
      Assert.IsNotNull(okNegotiatedContentResult);
      Assert.IsTrue(menu.SequenceEqual(okNegotiatedContentResult.Content));
    }
  }
}
