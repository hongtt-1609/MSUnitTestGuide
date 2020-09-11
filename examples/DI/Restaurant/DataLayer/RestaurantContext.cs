using RestaurantApp.Core;
using System;
using System.Data.Entity;

namespace RestaurantApp.Infrastructure
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext() : base("name=RestaurantAppConnectionString")
        {

        }
      

        public DbSet<Food> Food { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public void SetModified(object entity)
        {
            Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
