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
   public class CartRepositoryTest
    {
        [TestMethod]
        public void CreateItem_SaveItem_ViaContext()
        {
            var mockSet = new Mock<DbSet<Cart>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Cart).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.CartRepository(mockContext.Object);
            var fixture = new Fixture();
            var testObject = fixture.Build<Cart>().Without(c => c.Id).Create();
            repo.Update(testObject);
            //Assert
            mockSet.Verify(m => m.Add(It.Is<Cart>(c => c.Id == testObject.Id)), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Update()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<Cart>().With(c => c.Id, 1).Create<Cart>();
            var mockSet = new Mock<DbSet<Cart>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Cart).Returns(mockSet.Object);
            mockContext.Setup(m => m.SetModified(It.IsAny<Cart>()));
            var repo = new RestaurantApp.Infrastructure.Repositories.CartRepository(mockContext.Object);
            //Act
            repo.Update(testObject);
            //Assert
            mockContext.Verify(m => m.SetModified(It.IsAny<Cart>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void RemoveItemByIdWithValidData()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<Cart>().Create<Cart>();
            var mockSet = new Mock<DbSet<Cart>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Set<Cart>()).Returns(mockSet.Object); ;
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(testObject);
            mockSet.Setup(m => m.Remove(It.IsAny<Cart>())).Returns(testObject);
            var repo = new RestaurantApp.Infrastructure.Repositories.CartRepository(mockContext.Object);
            //act
            var result = repo.Remove(1);
            //Assert
            Assert.AreEqual(true, result);
            mockContext.Verify(m => m.Set<Cart>());
            mockSet.Verify(m => m.Find(It.IsAny<int>()));
            mockSet.Verify(m => m.Remove(It.IsAny<Cart>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

       

        [TestMethod]
        public void GetById()
        {
            var fixture = new Fixture();
            var data = new List<Cart>()
            {
                fixture.Build<Cart>().With(c=>c.Id,1).With(c=>c.Id,1).Create(),
                fixture.Build<Cart>().With(c=>c.Id,2).With(c=>c.Id,2).Create()
        }.AsQueryable();
            var mockSet = Helper.CreateDbSetMock<Cart>(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Cart).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.CartRepository(mockContext.Object);
            var food = repo.GetById(2);
            Assert.AreEqual(2, food.Id);

        }
    }
}
