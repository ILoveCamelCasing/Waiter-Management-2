using System;
using System.Linq;
using AutoMapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Objects;

namespace WaiterManagement.Common.Entities.Abstract
{
	public class VersionableEntity : IEntity
	{
		public int Id { get; private set; }
		public Guid CommonId { get; private set; }
		public DateTime Created { get; private set; }
		public DateTime? Modified { get; private set; }
		public int Version { get; private set; }
		public bool IsNewest { get; private set; }
		public bool IsDeleted { get; private set; }

		protected VersionableEntity()
		{
			CommonId = Guid.NewGuid();
			Created = SystemTime.Now;
			Version = 1;
			IsNewest = true;
			IsDeleted = false;
		}

		public virtual VersionableEntity CreateNewVersion(IUnitOfWork unitOfWork)
		{
			var type = ObjectContext.GetObjectType(GetType());
			var newVersion = (VersionableEntity)unitOfWork.Add(type,Clone());
			newVersion.Modified = SystemTime.Now;
			newVersion.Version++;

			this.IsNewest = false;

			return newVersion;
		}

		public virtual VersionableEntity Clone()
		{
			Mapper.CreateMap(GetType(), ObjectContext.GetObjectType(GetType()));
			return (VersionableEntity)Mapper.Map(this, GetType(), GetType());
		}

		public virtual VersionableEntity CreateDeletedVersion(IUnitOfWork unitOfWork)
		{
			var newVersion = CreateNewVersion(unitOfWork);
			newVersion.IsDeleted = true;

			return newVersion;
		}
	}
}