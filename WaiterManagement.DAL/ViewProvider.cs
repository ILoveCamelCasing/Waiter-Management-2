﻿using System;
using System.Linq;
using System.Threading.Tasks;
using WaiterManagement.Common.Views.Abstract;

namespace WaiterManagement.DAL
{
	public class ViewProvider : IViewProvider, IDisposable
	{
		private readonly WaiterManagementContext _db;

		public ViewProvider()
		{
			_db = new WaiterManagementContext();
		}

		public IQueryable<T> Get<T>() where T : class, IView
		{
			return _db.Set<T>();
    }

    async Task<IQueryable<T>> IViewProvider.GetAsync<T>()
    {
      return await Task.Run(() => Get<T>());
    }

    public void Dispose()
		{
			_db.Dispose();
		}
  }
}