using System;

namespace WaiterManagement.Common.Views.Abstract
{
	public interface ILoginableView : IView
	{
		string Login { get; }
		string SecondHash { get; }
		Guid UserId { get; }
	}
}