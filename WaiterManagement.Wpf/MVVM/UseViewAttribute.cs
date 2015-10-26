using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows.Controls;
using Caliburn.Micro;

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

		public static void ConfigureViewLocator()
		{
			ViewLocator.LocateForModelType = (modelType, displayLocation, context) =>
			{
				var useViewAttributes = modelType.GetCustomAttributes(typeof(UseViewAttribute), true)
					.Cast<UseViewAttribute>();

				Contract.Assert(useViewAttributes.Count() <= 1, "There can only be zero or one UseViewAttribute on a view model");

				string viewTypeName;

				if (useViewAttributes.Count() == 1)
					viewTypeName = string.Concat(modelType.Namespace.Substring(0,modelType.Namespace.IndexOf(".ViewModels")) , ".Views.",
						useViewAttributes.First().ViewName);
				else
				{
					viewTypeName = modelType.FullName.Replace("Model", string.Empty);
					if (context != null)
					{
						viewTypeName = viewTypeName.Remove(viewTypeName.Length - 4, 4);
						viewTypeName = viewTypeName + "." + context;
					}
				}

				var viewType = (from assembly in AssemblySource.Instance
								from type in assembly.GetExportedTypes()
								where type.FullName == viewTypeName
								select type).FirstOrDefault();

				return viewType == null
					? new TextBlock { Text = string.Format("{0} not found.", viewTypeName) }
					: ViewLocator.GetOrCreateViewType(viewType);
			};
		}
	}
}
