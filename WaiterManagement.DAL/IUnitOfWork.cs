using System;

namespace WaiterManagement.DAL
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		void Revert();
		void Add<T>(T item) where T : IEntity;
	}
}