using WaiterManagement.BLL.Commands.Base;

namespace WaiterManagement.BLL.Commands.Concrete
{
	public class EditTableCommand : ICommand
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
	}
}