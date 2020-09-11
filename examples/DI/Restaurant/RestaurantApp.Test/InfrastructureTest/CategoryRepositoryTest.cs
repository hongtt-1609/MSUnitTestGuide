using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Moq;
using RestaurantApp.Core;
using RestaurantApp.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AutoFixture;
namespace RestaurantApp.Test.InfrastructureTest
{
    // Implement fixture: auto create sample data.
    [TestClass]
    public class CategoryRepositoryTest
    {
        [TestMethod]
        public void CreateItem_SaveItem_ViaContext()
        {
            var mockSet = new Mock<DbSet<Category>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Category).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.CategoryRepository(mockContext.Object);
            var fixture = new Fixture();
            var testObject = fixture.Build<Category>().Without(c => c.Id).Create();
            repo.Update(testObject);
            //Assert
            mockSet.Verify(m => m.Add(It.Is<Category>(c=>c.Name==testObject.Name)), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Update()
        {
            var fixture = new Fixture();
           var testObject = fixture.Build<Category>().With(c => c.Id, 1).Create<Category>();
            var mockSet = new Mock<DbSet<Category>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Category).Returns(mockSet.Object);
            mockContext.Setup(m => m.SetModified(It.IsAny<Category>()));
            var repo = new RestaurantApp.Infrastructure.Repositories.CategoryRepository(mockContext.Object);
            //Act
            repo.Update(testObject);
            //Assert
            mockContext.Verify(m => m.SetModified(It.IsAny<Category>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

        }

        [TestMethod]
        public void RemoveItemByIdWithValidData()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<Category>().Create<Category>();
            var mockSet = new Mock<DbSet<Category>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Set<Category>()).Returns(mockSet.Object); ;
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(testObject);
            mockSet.Setup(m => m.Remove(It.IsAny<Category>())).Returns(testObject);
            var repo = new RestaurantApp.Infrastructure.Repositories.CategoryRepository(mockContext.Object);
            //act
            var result = repo.Remove(1);
            //Assert
            Assert.AreEqual(true, result);
            mockContext.Verify(m => m.Set<Category>());
            mockSet.Verify(m => m.Find(It.IsAny<int>()));
            mockSet.Verify(m => m.Remove(It.IsAny<Category>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetCategories_WithPaging_OrderByName()
        {
            var fixture = new Fixture();
            var data = new List<Category>()
            {
                fixture.Build<Category>().With(c=>c.Name,"seafood").Create(),
                fixture.Build<Category>().With(c=>c.Name,"cheese cake").Create()

            }.AsQueryable();
            var mockDbSet = Helper.CreateDbSetMock(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Category).Returns(mockDbSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.CategoryRepository(mockContext.Object);
            List<Category> result = repo.GetCategories(0, 2).ToList();
            //assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("cheese cake", result[0].Name);
            Assert.AreEqual("seafood", result[1].Name);

        }

        [TestMethod]
        public void GetById()
        {
            var fixture = new Fixture();
            var data = new List<Category>()
            {
                fixture.Build<Category>().With(c=>c.Id,1).With(c=>c.Name,"seafood").Create(),
                fixture.Build<Category>().With(c=>c.Id,2).With(c=>c.Name,"cheese cake").Create()
        }.AsQueryable();
            var mockSet = Helper.CreateDbSetMock<Category>(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Category).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.CategoryRepository(mockContext.Object);
            var food = repo.GetById(2);
            Assert.AreEqual("cheese cake", food.Name);

        }
    }
}
