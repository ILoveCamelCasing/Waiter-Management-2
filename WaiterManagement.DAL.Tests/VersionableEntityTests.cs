using System;
using NSubstitute;
using WaiterManagement.Common;
using WaiterManagement.Common.Entities.Abstract;
using Xunit;

namespace WaiterManagement.DAL.Tests
{
	public class VersionableEntityTests : IDisposable
	{
		[Fact]
		public void CreateFirstVersion()
		{
			// Arrange
			var now = DateTime.Now;
			SystemTime.SetTime(now);

			// Act
			var entity = new TestVersionableEntity();
			entity.SomeField = "someData";

			// Assert
			Assert.Equal(0,entity.Id);
			Assert.Equal(1,entity.Version);
			Assert.NotNull(entity.CommonId);
			Assert.Equal(false,entity.IsDeleted);
			Assert.Equal(true,entity.IsNewest);
			Assert.Equal(now,entity.Created);
			Assert.Null(entity.Modified);
			Assert.Equal("someData",entity.SomeField);
		}

		[Fact]
		public void CreateNewVersion()
		{
			// Arrange
			var firstTime = DateTime.Now;
			SystemTime.SetTime(firstTime);
			var entity = new TestVersionableEntity();
			entity.SomeField = "someData";

			var secondTime = DateTime.Now;
			SystemTime.SetTime(secondTime);
			
			var unitOfWorkMock = Substitute.For<IUnitOfWork>();
			unitOfWorkMock.Add(Arg.Any<Type>(), Arg.Any<TestVersionableEntity>()).Returns(x => x.Args()[1]);

			// Act
			var newVersion = (TestVersionableEntity)entity.CreateNewVersion(unitOfWorkMock);

			// Assert
			Assert.Equal(0, newVersion.Id);
			Assert.Equal(2, newVersion.Version);
			Assert.Equal(entity.CommonId,newVersion.CommonId);
			Assert.Equal(false, newVersion.IsDeleted);
			Assert.Equal(true, newVersion.IsNewest);
			Assert.Equal(firstTime, newVersion.Created);
			Assert.Equal(secondTime, newVersion.Modified);
			Assert.Equal("someData",newVersion.SomeField);

			Assert.Equal(false, entity.IsNewest);
		}

		public void Dispose()
		{
			SystemTime.Reset();
		}

		private class TestVersionableEntity : VersionableEntity
		{
			public string SomeField { get; set; }
		}
	}
}
