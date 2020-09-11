using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantApp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Moq;
using AutoFixture;

namespace RestaurantApp.Test.InfrastructureTest
{
    [TestClass]
    public class OrderRepositoryTest
    {
        [TestMethod]
        public void CreateItem_SaveItem_ViaContext()
        {
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Order).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.OrderRepository(mockContext.Object);
            var fixture = new Fixture();
            var testObject = fixture.Build<Order>().Without(c => c.Id).Create();
            repo.Update(testObject);
            //Assert
            mockSet.Verify(m => m.Add(It.Is<Order>(c => c.Id == testObject.Id)), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Update()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<Order>().With(c => c.Id, 1).Create<Order>();
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Order).Returns(mockSet.Object);
            mockContext.Setup(m => m.SetModified(It.IsAny<Order>()));
            var repo = new RestaurantApp.Infrastructure.Repositories.OrderRepository(mockContext.Object);
            //Act
            repo.Update(testObject);
            //Assert
            mockContext.Verify(m => m.SetModified(It.IsAny<Order>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void RemoveItemByIdWithValidData()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<Order>().Create<Order>();
            var mockSet = new Mock<DbSet<Order>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Set<Order>()).Returns(mockSet.Object); ;
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(testObject);
            mockSet.Setup(m => m.Remove(It.IsAny<Order>())).Returns(testObject);
            var repo = new RestaurantApp.Infrastructure.Repositories.OrderRepository(mockContext.Object);
            //act
            var result = repo.Remove(1);
            //Assert
            Assert.AreEqual(true, result);
            mockContext.Verify(m => m.Set<Order>());
            mockSet.Verify(m => m.Find(It.IsAny<int>()));
            mockSet.Verify(m => m.Remove(It.IsAny<Order>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetOrders_WithPaging_OrderById()
        {
            var fixture = new Fixture();
            var data = new List<Order>()
            {
                fixture.Build<Order>().With(c=>c.Id,1).Create(),
                fixture.Build<Order>().With(c=>c.Id,2).Create()

            }.AsQueryable();
            var mockDbSet = Helper.CreateDbSetMock(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Order).Returns(mockDbSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.OrderRepository(mockContext.Object);
            List<Order> result = repo.GetOrders(0, 2).ToList();
            //assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result[0].Id);
            Assert.AreEqual(1, result[1].Id);

        }

        [TestMethod]
        public void GetById()
        {
            var fixture = new Fixture();
            var data = new List<Order>()
            {
                fixture.Build<Order>().With(c=>c.Id,1).With(c=>c.Id,1).Create(),
                fixture.Build<Order>().With(c=>c.Id,2).With(c=>c.Id,2).Create()
        }.AsQueryable();
            var mockSet = Helper.CreateDbSetMock<Order>(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Order).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.OrderRepository(mockContext.Object);
            var food = repo.GetById(2);
            Assert.AreEqual(2, food.Id);

        }
    }
}
