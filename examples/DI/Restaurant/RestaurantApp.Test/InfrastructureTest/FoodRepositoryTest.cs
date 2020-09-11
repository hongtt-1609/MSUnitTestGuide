using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Collections.Generic;
using RestaurantApp.Core;
using Moq;
using System.Linq;

namespace RestaurantApp.Test.InfrastructureTest
{
    //Test with mock DBSet & mock DBContext to test action CRUD, query from database - Basic
    [TestClass]
    public class FoodRepositoryTest
    {
        [TestMethod]
        public void CreateItem_SaveItem_Via_Context()
        {
            var mockSet = new Mock<DbSet<RestaurantApp.Core.Food>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Food).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.FoodRepository(mockContext.Object);
            var food = new Food()
            {
                Name = "Teriyaki bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UnitId = 1,
                Image = "FoodTest.jpg",
                Description = "Teriyaki bugger"
            };
            repo.Update(food);
            mockSet.Verify(m => m.Add(It.Is<Food>(f=>f.Name== "Teriyaki bugger")), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void GetFoodById()
        {
            var data = new List<Food>()
            {
                new Food()
            {
                Id = 1,
                Name = "Teriyaki bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,00,00),
                UpdatedAt =  new DateTime(2020,4,20,17,00,00,00),
                UnitId = 1,
                Image = "FoodTest.jpg",
                Description = "Teriyaki bugger"
            },
                new Food()
            {
                Id=2,
                Name = "seafood bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,01,00),
                UpdatedAt = new DateTime(2020,4,20,17,00,01,00),
                UnitId = 1,
                Image = "seafoodjpg",
                Description = "seafood bugger"
            }
        }.AsQueryable();
            var mockSet = new Mock<DbSet<RestaurantApp.Core.Food>>();

            mockSet.As<IQueryable<Food>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Food>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Food>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Food>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Food).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.FoodRepository(mockContext.Object);
            var food = repo.GetById(2);
            Assert.AreEqual("seafood bugger", food.Name);

        }

        [TestMethod]
        public void GetAllFoodWithPagingOrderByName()
        {
            var data = new List<Food>()
            {
                new Food()
            {
                Name = "Teriyaki bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,00,00),
                UpdatedAt =  new DateTime(2020,4,20,17,00,00,00),
                UnitId = 1,
                Image = "FoodTest.jpg",
                Description = "Teriyaki bugger"
            },
                new Food()
            {
                Name = "seafood bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,01,00),
                UpdatedAt = new DateTime(2020,4,20,17,00,01,00),
                UnitId = 1,
                Image = "seafoodjpg",
                Description = "seafood bugger"
            },
                new Food()
            {
                Name = "alibaba bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Drink,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,02,00),
                UpdatedAt =new DateTime(2020,4,20,17,00,02,00),
                UnitId = 1,
                Image = "alibaba.jpg",
                Description = "alibaba bugger"
            }
        }.AsQueryable();
            var mockSet = new Mock<DbSet<RestaurantApp.Core.Food>>();
            mockSet.As<IQueryable<Food>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Food>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Food>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Food>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Food).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.FoodRepository(mockContext.Object);
            List<Food> foods = repo.GetFoods(0,2).ToList();
            Assert.AreEqual(2, foods.Count());
            Assert.AreEqual("alibaba bugger", foods[0].Name);
            Assert.AreEqual("seafood bugger", foods[1].Name);

        }

        [TestMethod]
        public void GetFoodsByTypeWithPaging_OrderByName()
        {
            var data = new List<Food>()
            {
                new Food()
            {
                Name = "Teriyaki bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,00,00),
                UpdatedAt =  new DateTime(2020,4,20,17,00,00,00),
                UnitId = 1,
                Image = "FoodTest.jpg",
                Description = "Teriyaki bugger"
            },
                new Food()
            {
                Name = "seafood bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,01,00),
                UpdatedAt = new DateTime(2020,4,20,17,00,01,00),
                UnitId = 1,
                Image = "seafoodjpg",
                Description = "seafood bugger"
            },
                new Food()
            {
                Name = "alibaba bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020,4,20,17,00,02,00),
                UpdatedAt =new DateTime(2020,4,20,17,00,02,00),
                UnitId = 1,
                Image = "alibaba.jpg",
                Description = "alibaba bugger"
            }
        }.AsQueryable();
            var mockSet = new Mock<DbSet<RestaurantApp.Core.Food>>();
            mockSet.As<IQueryable<Food>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Food>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Food>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Food>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(m => m.Food).Returns(mockSet.Object);
            var repo = new RestaurantApp.Infrastructure.Repositories.FoodRepository(mockContext.Object);
            List<Food> foods = repo.GetFoods((int)FoodType.Food,0,2).ToList();
            Assert.AreEqual(2, foods.Count());
            Assert.AreEqual("alibaba bugger", foods[0].Name);
            Assert.AreEqual("seafood bugger", foods[1].Name);
        }

        [TestMethod]
        public void RemoveItemWithValidData()
        {
            //Arange 
            var testObject = new RestaurantApp.Core.Food()
            {
                Id = 1,
                Name = "Teriyaki bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020, 4, 20, 17, 00, 00, 00),
                UpdatedAt = new DateTime(2020, 4, 20, 17, 00, 00, 00),
                UnitId = 1,
                Image = "FoodTest.jpg",
                Description = "Teriyaki bugger"
            };
            int idToRemove = 1;
            var mockSet = new Mock<DbSet<RestaurantApp.Core.Food>>();
            var mockContext = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            mockContext.Setup(x => x.Set<RestaurantApp.Core.Food>()).Returns(mockSet.Object);
            mockSet.Setup(x => x.Find(It.IsAny<int>())).Returns(testObject);
            mockSet.Setup(x => x.Remove(It.IsAny<RestaurantApp.Core.Food>())).Returns(testObject);
            //Act
            var repo = new RestaurantApp.Infrastructure.Repositories.FoodRepository(mockContext.Object);
            var result = repo.Remove(idToRemove);
            //Assert
            Assert.AreEqual(true, result);
            mockContext.Verify(x => x.Set<RestaurantApp.Core.Food>());
            mockSet.Verify(x => x.Find(It.IsAny<int>()));
            mockSet.Verify(x => x.Remove(It.Is<RestaurantApp.Core.Food>(y => y == testObject)));
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void Update()
        {
            var testObject = new RestaurantApp.Core.Food()
            {
                Id = 1,
                Name = "Teriyaki bugger",
                CategoryId = 2,
                Amount = 10,
                Point = 0,
                Type = (int)FoodType.Food,
                Price = 15000,
                CreatedAt = new DateTime(2020, 4, 20, 17, 00, 00, 00),
                UpdatedAt = new DateTime(2020, 4, 20, 17, 00, 00, 00),
                UnitId = 1,
                Image = "FoodTest.jpg",
                Description = "Teriyaki bugger"
            };
            var dbSetMock = new Mock<DbSet<RestaurantApp.Core.Food>>();
            var dbContextMock = new Mock<RestaurantApp.Infrastructure.VitualRestaurantContext>();
            dbContextMock.Setup(m => m.Set<RestaurantApp.Core.Food>()).Returns(dbSetMock.Object);
            dbContextMock.Setup(m => m.SetModified(It.IsAny<RestaurantApp.Core.Food>()));
            var repo = new RestaurantApp.Infrastructure.Repositories.FoodRepository(dbContextMock.Object);
            repo.Update(testObject);
            //Assert
            dbContextMock.Verify(m => m.SetModified(It.IsAny<RestaurantApp.Core.Food>()), Times.Once);
            dbContextMock.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
