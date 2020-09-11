using System.Data.Entity;
using RestaurantApp.Core;
using System;

namespace RestaurantApp.Infrastructure
{
    public class RestaurantInitalizeDB : DropCreateDatabaseIfModelChanges<RestaurantContext>
    {
        protected override void Seed(RestaurantContext context)
        {
            context.Category.Add(new Category
            {
                Id=1,
                Name="Fast food",
                CreatedAt =DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.Category.Add(new Category
            {
                Id = 2,
                Name = "fresh food",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.Category.Add(new Category
            {
                Id = 3,
                Name = "Takeaway",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.Category.Add(new Category
            {
                Id = 4,
                Name = "Seafood",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.Category.Add(new Category
            {
                Id = 5,
                Name = "Cake",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.Unit.Add(new Unit
            {
                Id = 1,
                Name = "Kg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.Unit.Add(new Unit
            {
                Id = 2,
                Name = "Package",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.Food.Add(new Food
            {
                Id = 1,
                Name = "cheese cake",
                CategoryId = 5,
                Amount = 5,
                Point= 0,
                Type  = (int)FoodType.Food,
                Image="\\WebImages\\Foods\\1\\temp.jpg",
                Price=35000,
                UnitId = 2,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            });
            context.Food.Add(new Food
            {
                Id = 2,
                Name = "Envy Apple",
                CategoryId = 2,
                Amount = 5,
                Point = 0,
                Type = (int)FoodType.Food,
                Image = "\\WebImages\\Foods\\2\\temp.jpg",
                Price = 250000,
                UnitId=1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            });
            context.User.Add(new User
            {
                Id = 1,
                Name = "Trinh Thu Hong",
                Password = Utility.PasswordHelp.Md5Encrypt("123456"),
                Age = 25,
                Gender = (int)Gender.Female,
                Permission = (int)UserRole.admin,
                image = "\\WebImages\\Users\\1\\user.jpg",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            context.SaveChanges();
            base.Seed(context);
        }
    }
  
}
