using System;
using System.Threading.Tasks;

namespace WaiterManagement.Common.Entities.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		void Revert();
		void Add(Type entityType, object item);
		void Add<T>(T item) where T : class, IEntity;
		Task AddAsync<T>(T item) where T : class, IEntity;
		T Get<T>(int id) where T : class, IEntity;
		Task<T> GetAsync<T>(int id) where T : class, IEntity;
	}
}