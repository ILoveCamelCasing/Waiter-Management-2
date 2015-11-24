using System;
using System.Linq.Expressions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.BLL.Commands.Handlers;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using WaiterManagement.Common.Security;
using Xunit;

namespace WaiterManagement.BLL.Tests.Handlers
{
	public class AddTableHandlerTests
	{
		private IUnitOfWork _unitOfWorkMock;
		public IUnitOfWork UnitOfWorkMock 
		{
			get
			{
				return _unitOfWorkMock ?? (_unitOfWorkMock = Substitute.For<IUnitOfWork>());
			}
		}

		private IPasswordManager _passwordManagerMock;
		public IPasswordManager PasswordManagerMock
		{
			get
			{
				return _passwordManagerMock ?? (_passwordManagerMock = Substitute.For<IPasswordManager>());
			}
		}

		private void Execute()
		{
			var command = new AddTableCommand() {Title = "Title1", Description = "Description1", Password = "P@ssw0rd"};
			Execute(command);
		}

		private void Execute(AddTableCommand command)
		{
			var handler = new AddTableHandler(UnitOfWorkMock,PasswordManagerMock);
			handler.Handle(command);
		}

		[Theory]
		[AutoData]
		public void add_table_correctly(AddTableCommand command)
		{
			Execute(command);

			UnitOfWorkMock.Received().Add(Arg.Is<Table>(x => 
				x.Title == command.Title && 
				x.Description == command.Description));
		}

		[Theory]
		[AutoData]
		public void add_user_with_table_correctly(AddTableCommand command)
		{
			PasswordManagerMock.CreateSecondHash(Arg.Any<string>(), Arg.Any<string>()).Returns("xyz");

			Execute(command);

			UnitOfWorkMock.Received().Add(Arg.Is<User>(x =>
				x.Login == command.Title && x.SecondHash == "xyz"));
		}

		[Theory]
		[AutoData]
		public void add_table_with_same_name_faild(AddTableCommand command)
		{
			UnitOfWorkMock.AnyActual(
				Arg.Any<Expression<Func<Table, bool>>>()).Returns(true);

			Assert.Throws<InvalidOperationException>(() => Execute());
		}
	}
}