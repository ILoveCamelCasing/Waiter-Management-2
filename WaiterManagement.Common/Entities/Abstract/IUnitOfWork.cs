using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WaiterManagement.Common.Entities.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		void Commit();
		void Revert();
		object Add(Type entityType, object item);
		T Add<T>(T item) where T : class, IEntity;
		bool AnyActual<T>(Expression<Func<T, bool>> predicate) where T : VersionableEntity;
		Task AddAsync<T>(T item) where T : class, IEntity;
		T Get<T>(int id) where T : class, IEntity;
		T Get<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity;
		object Get(Type entityType, int id);
		T GetFirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity;
		IQueryable<T> GetWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity;
		T GetActual<T>(Expression<Func<T, bool>> predicate) where T : VersionableEntity;
		void Load<TEntity, TProperty>(TEntity item, Expression<Func<TEntity, TProperty>> navigationProperty)
			where TProperty : class
			where TEntity : class;

		void Load<TEntity, TElement>(TEntity item,
			Expression<Func<TEntity, System.Collections.Generic.ICollection<TElement>>> navigationProperty)
			where TEntity : class
			where TElement : class;
		Task<T> GetAsync<T>(int id) where T : class, IEntity;
	}
}