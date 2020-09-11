using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantApp.Core;
using RestaurantApp.Infrastructure;

namespace RestaurantApp.Test.InfrastructureTest
{
    //Test with mock DBSet & mock DBContext to test action CRUD, query from database - with helper
    [TestClass]
    public class UnitRepositoryTest
    {
        [TestMethod]
        public void CreateItem_UpdateItem_Via_Context()
        {
            var mockSet = new Mock<DbSet<Unit>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Unit).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.UnitRepository(mockContext.Object);
            var testObject = new Unit()
            {
                Name = "Piece"
            };
            repo.Update(testObject);
            mockSet.Verify(m => m.Add(It.Is<RestaurantApp.Core.Unit>(u => u.Name == "Piece")), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [TestMethod]
        public void UpdateItem_Via_Context()
        {
            var testObject = new Unit()
            {
                Id = 1,
                Name = "Piece"
            };
            var mockSet = new Mock<DbSet<Unit>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Unit).Returns(mockSet.Object);
            mockContext.Setup(m => m.SetModified(It.IsAny<Unit>()));
            var repo = new RestaurantApp.Infrastructure.Repositories.UnitRepository(mockContext.Object);
            repo.Update(testObject);
            mockContext.Verify(m => m.SetModified(It.IsAny<Unit>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void RemoteById()
        {
            var testObject = new Unit()
            {
                Id = 1,
                Name = "Piece"
            };
            int idToDelete = 1;
            var mockSet = new Mock<DbSet<Unit>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Set<Unit>()).Returns(mockSet.Object);
            mockSet.Setup(m => m.Find(It.IsAny<int>())).Returns(testObject);
            mockSet.Setup(m => m.Remove(It.IsAny<Unit>())).Returns(testObject);

            var repo = new RestaurantApp.Infrastructure.Repositories.UnitRepository(mockContext.Object);
           var result =  repo.Remove(idToDelete);
            //Assert
            Assert.AreEqual(true, result);
            mockContext.Verify(m => m.Set<Unit>());
            mockSet.Verify(m => m.Find(It.IsAny<int>()));
            mockSet.Verify(m => m.Remove(It.IsAny<Unit>()));
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void GetUnits_WithPaging_OrderById()
        {
            var data = new List<Unit>()
            {
                new Unit
                {
                    Id=1,
                    Name = "Kg"
                },
                new Unit
                {
                    Id=2,
                    Name = "Piece"
                },
                new Unit
                {
                    Id=3,
                    Name = "bag"
                }
            }.AsQueryable();
            var mockDbSet = Helper.CreateDbSetMock(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Unit).Returns(mockDbSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.UnitRepository(mockContext.Object);
            List<Unit> result = repo.GetUnits(0, 2).ToList();
            //assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("bag", result[0].Name);
            Assert.AreEqual("Piece", result[1].Name);

        }

        [TestMethod]
        public void GetById()
        {
            var data = new List<Unit>()
            {
               new Unit
                {
                    Id=1,
                    Name = "Kg"
                },
                new Unit
                {
                    Id=2,
                    Name = "Piece"
                }
        }.AsQueryable();
            var mockSet = Helper.CreateDbSetMock<Unit>(data);
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Unit).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.UnitRepository(mockContext.Object);
            var food = repo.GetById(2);
            Assert.AreEqual("Piece", food.Name);

        }
    }
}
