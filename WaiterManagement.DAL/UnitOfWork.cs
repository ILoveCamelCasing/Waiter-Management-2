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

		public bool AnyActual<T>(Expression<Func<T, bool>> predicate) where T : VersionableEntity
		{
			Expression<Func<T, bool>> isActualExpression = entity => entity.IsNewest && !entity.IsDeleted;
			return _dbContext.Set<T>().Any(AndAlso(predicate,isActualExpression));
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

		// TODO: Przenieść do oddzielnej klasy jako metoda rozszerzająca
		private static Expression<Func<T, bool>> AndAlso<T>(
			Expression<Func<T, bool>> expr1,
			Expression<Func<T, bool>> expr2)
		{
			var parameter = Expression.Parameter(typeof(T));

			var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
			var left = leftVisitor.Visit(expr1.Body);

			var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
			var right = rightVisitor.Visit(expr2.Body);

			return Expression.Lambda<Func<T, bool>>(
				Expression.AndAlso(left, right), parameter);
		}

		private class ReplaceExpressionVisitor
			: ExpressionVisitor
		{
			private readonly Expression _oldValue;
			private readonly Expression _newValue;

			public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
			{
				_oldValue = oldValue;
				_newValue = newValue;
			}

			public override Expression Visit(Expression node)
			{
				if (node == _oldValue)
					return _newValue;
				return base.Visit(node);
			}
		}
	}
}