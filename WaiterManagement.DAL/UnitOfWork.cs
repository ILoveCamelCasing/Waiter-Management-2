using System;
using System.Data.Entity;
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

		public void Add<T>(T item) where T : class, IEntity
		{
			_dbContext.Set<T>().Add(item);
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