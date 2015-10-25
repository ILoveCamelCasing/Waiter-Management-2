using System;

namespace WaiterManagement.Common.Entities.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		void Revert();
		void Add<T>(T item) where T : class, IEntity;
		T Get<T>(int id) where T : class, IEntity;
	}
}