using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantApp.Core;
using RestaurantApp.Core.Interfaces;
using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using RestaurantApp.Web;
using System.Web.Mvc;

namespace RestaurantApp.Web.Test
{
    [TestClass]
    public class FoodsControllerTest
    {
        [TestMethod]
        public void Index_ReturnsAViewResult_WithAListOfFoodsNoPaging()
        {
            var fixture = new Fixture();
            var testObject = new List<Food> {
                fixture.Build<Food>().With(f=>f.Type,1).Create(),
                fixture.Build<Food>().With(f=>f.Type,1).Create(),
                fixture.Build<Food>().With(f=>f.Type,2).Create(),
                fixture.Build<Food>().With(f=>f.Type,2).Create(),
                fixture.Build<Food>().With(f=>f.Type,2).Create(),
                fixture.Build<Food>().With(f=>f.Type,1).Create(),
                fixture.Build<Food>().With(f=>f.Type,1).Create()
            };
            var repMock = new Mock<IFoodRepository>();
            int pageSize = 5;
            int page = 0;
            int type = 0;
            repMock.Setup(m => m.GetFoods(type,page,pageSize)).Returns(testObject.AsQueryable());
            var controller = new Controllers.FoodsController(repMock.Object);
            var result = controller.Index(type, page) as ViewResult;
            var model = result.Model as List<Food>;
            Assert.IsNotNull(result);
            Assert.AreEqual(5, model.Count());

        }
    }
}
