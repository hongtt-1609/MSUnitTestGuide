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
    public class CartDetailDetailRepositoryTest
    {
        [TestMethod]
        public void CreateItem_SaveItem_ViaContext()
        {
            var mockSet = new Mock<DbSet<CartDetail>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.CartDetail).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.CartDetailRepository(mockContext.Object);
            var fixture = new Fixture();
            var testObject = fixture.Build<CartDetail>().Without(c => c.Id).Create();
            repo.Update(testObject);
            //Assert
            mockSet.Verify(m => m.Add(It.Is<CartDetail>(c => c.Id == testObject.Id)), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Update()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<CartDetail>().With(c => c.Id, 1).Create<CartDetail>();
            var mockSet = new Mock<DbSet<CartDetail>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.CartDetail).Returns(mockSet.Object);
            mockContext.Setup(m => m.SetModified(It.IsAny<CartDetail>()));
            var repo = new RestaurantApp.Infrastructure.Repositories.CartDetailRepository(mockContext.Object);
            //Act
            repo.Update(testObject);
            //Assert
            mockContext.Verify(m => m.SetModified(It.IsAny<CartDetail>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void RemoveItemByIdWidthValidData()
        {
            var fixture = new Fixture();
            var testObject = fixture.Build<CartDetail>().Create<CartDetail>();
            var mockSet = new Mock<DbSet<CartDetail>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Set<CartDetail>()).Returns(mockSet.Object); ;
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(testObject);
            mockSet.Setup(m => m.Remove(It.IsAny<CartDetail>())).Returns(testObject);
            var repo = new RestaurantApp.Infrastructure.Repositories.CartDetailRepository(mockContext.Object);
            //act
            var result = repo.Remove(1);
            //Assert
            Assert.AreEqual(true, result);
            mockContext.Verify(m => m.Set<CartDetail>());
            mockSet.Verify(m => m.Find(It.IsAny<int>()));
            mockSet.Verify(m => m.Remove(It.IsAny<CartDetail>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }



        [TestMethod]
        public void GetById()
        {
            var fixture = new Fixture();
            var data = new List<CartDetail>()
            {
                fixture.Build<CartDetail>().With(c=>c.Id,1).With(c=>c.Id,1).Create(),
                fixture.Build<CartDetail>().With(c=>c.Id,2).With(c=>c.Id,2).Create()
        }.AsQueryable();
            var mockSet = Helper.CreateDbSetMock<CartDetail>(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.CartDetail).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.CartDetailRepository(mockContext.Object);
            var food = repo.GetById(2);
            Assert.AreEqual(2, food.Id);

        }
    }
}
