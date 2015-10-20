using System;
using AutoMapper;
using WaiterManagement.Common;

namespace WaiterManagement.DAL
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
			CommonId = new Guid();
			Created = SystemTime.Now;
			Version = 1;
			IsNewest = true;
			IsDeleted = false;
		}

		public virtual VersionableEntity CreateNewVersion(IUnitOfWork unitOfWork)
		{
			Mapper.CreateMap(GetType(), GetType());
			var newVersion = (VersionableEntity)Mapper.Map(this, GetType(), GetType());
			newVersion.Modified = SystemTime.Now;
			newVersion.Version++;

			this.IsNewest = false;
			
			return newVersion;
		}
	}
}