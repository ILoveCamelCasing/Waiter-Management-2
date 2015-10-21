using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using WaiterManagement.BLL.Commands.Concrete;
using WaiterManagement.BLL.Commands.Handlers;
using WaiterManagement.Common.Entities;
using WaiterManagement.Common.Entities.Abstract;
using Xunit;

namespace WaiterManagement.BLL.Tests.Handlers
{
	public class AddTableHandlerTests
	{
		[Theory]
		[AutoData]
		public void add_table_correctly(AddTableCommand command)
		{
			// Arrange
			var unitOfWork = Substitute.For<IUnitOfWork>();

			// Act
			var handler = new AddTableHandler(unitOfWork);
			handler.Handle(command);

			// Assert
			unitOfWork.Received().Add(Arg.Is<Table>(x => 
				x.Title == command.Title && 
				x.Description == command.Description));
		}
	}
}