using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WaiterManagement.Common.Entities.Abstract;

namespace WaiterManagement.DAL
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly WaiterManagementContext _dbContext;

		public UnitOfWork()
		{
			_dbContext = new WaiterManagementContext();
		}

		public object Add(Type entityType, object item)
		{
			return _dbContext.Set(entityType).Add(item);
		}

		public T Add<T>(T item) where T : class, IEntity
		{
			return _dbContext.Set<T>().Add(item);
		}

		async Task IUnitOfWork.AddAsync<T>(T item)
		{
			await Task.Run(() => Add(item));
		}

		public T Get<T>(int id) where T : class, IEntity
		{
			return _dbContext.Set<T>().First(x => x.Id == id);
		}

		public void Load<TEntity, TProperty>(TEntity item, Expression<Func<TEntity, TProperty>> navigationProperty) 
			where TProperty : class 
			where TEntity : class
		{
			_dbContext.Entry(item).Reference(navigationProperty).Load();
		}

		public void Load<TEntity, TElement>(TEntity item, Expression<Func<TEntity, System.Collections.Generic.ICollection<TElement>>> navigationProperty)
			where TEntity : class
			where TElement : class
		{
			_dbContext.Entry(item).Collection(navigationProperty).Load();
		}

		async Task<T> IUnitOfWork.GetAsync<T>(int id)
		{
			return await Task.Run(() => Get<T>(id));
		}

		public void Commit()
		{
			_dbContext.SaveChanges();
		}

		public void Revert()
		{
			foreach (var entry in _dbContext.ChangeTracker.Entries())
			{
				switch (entry.State)
				{
					case EntityState.Modified:
						{
							entry.CurrentValues.SetValues(entry.OriginalValues);
							entry.State = EntityState.Unchanged;
							break;
						}
					case EntityState.Deleted:
						{
							entry.State = EntityState.Unchanged;
							break;
						}
					case EntityState.Added:
						{
							entry.State = EntityState.Detached;
							break;
						}
					case EntityState.Detached:
						break;
					case EntityState.Unchanged:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}