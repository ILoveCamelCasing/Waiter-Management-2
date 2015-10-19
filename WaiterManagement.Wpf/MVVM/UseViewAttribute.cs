using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace WaiterManagement.Wpf.MVVM
{
	public sealed class UseViewAttribute : Attribute
	{
		private readonly string viewName;

		public UseViewAttribute(string viewName)
		{
			Contract.Requires(!string.IsNullOrEmpty(viewName));
			Contract.Ensures(ViewName != null);
			this.viewName = viewName;
		}

		[ContractInvariantMethod]
		private void ObjectInvariant()
		{
			Contract.Invariant(ViewName != null);
		}

		public string ViewName { [DebuggerStepThrough] get { return viewName; } }
	}
}
