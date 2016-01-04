using System;
using System.Linq.Expressions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Concrete.ManagerCommands;
using WaiterManagement.BLL.Commands.Handlers;
using WaiterManagement.BLL.Commands.Handlers.ManagerHandlers;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using Xunit;

namespace WaiterManagement.BLL.Tests.Handlers
{
	public class EditTableHandlerTests
	{
		private bool _blockMock = false;

		private IUnitOfWork _unitOfWorkMock;
		public IUnitOfWork UnitOfWorkMock
		{
			get
			{
				return _unitOfWorkMock ?? (_unitOfWorkMock = Substitute.For<IUnitOfWork>());
			}
		}

		private Table _edited;
		public Table Edited
		{
			get { return _edited ?? (_edited = new Table() { Title = "xxxxx", Description = "yyyyy", User = EditedUser }); }
		}

		private User _editedUser;
		public User EditedUser
		{
			get { return _editedUser ?? (_editedUser = new User() { Login = "xxxxx" }); }
		}

		private void Execute()
		{
			var command = new EditTableCommand() { Title = "Title1", Description = "Description1" };
			Execute(command);
		}

		private void Execute(EditTableCommand command)
		{
			if (!_blockMock)
			{
				UnitOfWorkMock.Get<Table>(Arg.Any<int>()).Returns(Edited);
				UnitOfWorkMock.Add(Arg.Any<Type>(), Arg.Any<Table>()).Returns(Edited);
				UnitOfWorkMock.Add(Arg.Any<Type>(), Arg.Any<User>()).Returns(EditedUser);
			}
			var handler = new EditTableHandler(){UnitOfWork = UnitOfWorkMock};
			handler.Handle(command);
		}

		[Theory]
		[AutoData]
		public void edit_table_correctly(EditTableCommand command)
		{
			Execute(command);

			Assert.Equal(command.Title, Edited.Title);
			Assert.Equal(command.Description, Edited.Description);
		}

		[Theory]
		[AutoData]
		public void edit_login_with_edit_table_correctly(EditTableCommand command)
		{
			Execute(command);

			Assert.Equal(command.Title, EditedUser.Login);
		}

		[Fact]
		public void edit_table_by_existing_name_failed()
		{
			UnitOfWorkMock.AnyActual(
				Arg.Any<Expression<Func<Table, bool>>>()).Returns(true);

			Assert.Throws<InvalidOperationException>(() => Execute());
		}

		[Fact]
		public void edit_not_newest_table_failed()
		{
			var notNewestTable = new Table();
			typeof(VersionableEntity)
				.GetProperty("IsNewest")
				.SetValue(notNewestTable,false);

			UnitOfWorkMock.Get<Table>(Arg.Any<int>()).Returns(notNewestTable);
			_blockMock = true;

			Assert.Throws<InvalidOperationException>(() => Execute());
		}

		[Fact]
		public void edit_deleted_table_failed()
		{
			var deletedTable = new Table();
			typeof(VersionableEntity)
				.GetProperty("IsDeleted")
				.SetValue(deletedTable, true);

			UnitOfWorkMock.Get<Table>(Arg.Any<int>()).Returns(deletedTable);
			_blockMock = true;

			Assert.Throws<InvalidOperationException>(() => Execute());
		}
	}
}