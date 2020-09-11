using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Core;
using System.Data.Entity;
using Moq;
using AutoFixture;

namespace RestaurantApp.Test.InfrastructureTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void CreateItem_SaveItem_ViaContext()
        {
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.User).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.UserRepository(mockContext.Object);
            var fixture = new Fixture();
            var testObject = fixture.Build<User>().Without(c => c.Id).Create();
            repo.Update(testObject);
            //Assert
            mockSet.Verify(m => m.Add(It.Is<User>(c => c.Name == testObject.Name)), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Update()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<User>().With(c => c.Id, 1).Create<User>();
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.User).Returns(mockSet.Object);
            mockContext.Setup(m => m.SetModified(It.IsAny<User>()));
            var repo = new RestaurantApp.Infrastructure.Repositories.UserRepository(mockContext.Object);
            //Act
            repo.Update(testObject);
            //Assert
            mockContext.Verify(m => m.SetModified(It.IsAny<User>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void RemoveItemByIdWithValidData()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<User>().Create<User>();
            var mockSet = new Mock<DbSet<User>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Set<User>()).Returns(mockSet.Object); ;
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(testObject);
            mockSet.Setup(m => m.Remove(It.IsAny<User>())).Returns(testObject);
            var repo = new RestaurantApp.Infrastructure.Repositories.UserRepository(mockContext.Object);
            //act
            var result = repo.Remove(1);
            //Assert
            Assert.AreEqual(true, result);
            mockContext.Verify(m => m.Set<User>());
            mockSet.Verify(m => m.Find(It.IsAny<int>()));
            mockSet.Verify(m => m.Remove(It.IsAny<User>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetUsers_WithPaging_OrderByName()
        {
            var fixture = new Fixture();
            var data = new List<User>()
            {
                fixture.Build<User>().With(c=>c.Name,"Miley").Create(),
                fixture.Build<User>().With(c=>c.Name,"Isabella").Create()

            }.AsQueryable();
            var mockDbSet = Helper.CreateDbSetMock(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.User).Returns(mockDbSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.UserRepository(mockContext.Object);
            List<User> result = repo.GetUsers(0, 2).ToList();
            //assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Isabella", result[0].Name);
            Assert.AreEqual("Miley", result[1].Name);

        }

        [TestMethod]
        public void GetById()
        {
            var fixture = new Fixture();
            var data = new List<User>()
            {
                fixture.Build<User>().With(c=>c.Id,1).With(c=>c.Name,"seafood").Create(),
                fixture.Build<User>().With(c=>c.Id,2).With(c=>c.Name,"cheese cake").Create()
        }.AsQueryable();
            var mockSet = Helper.CreateDbSetMock<User>(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.User).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.UserRepository(mockContext.Object);
            var food = repo.GetById(2);
            Assert.AreEqual("cheese cake", food.Name);

        }
    }
}
