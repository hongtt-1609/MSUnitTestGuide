using System;
using System.Data.Entity;
using RestaurantApp.Core;

namespace RestaurantApp.Infrastructure
{
    public class VitualRestaurantContext:DbContext
    {
       

        public VitualRestaurantContext() : base("name=RestaurantAppConnectionString")
        {
            Configuration.LazyLoadingEnabled = true;
        }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartDetail> CartDetail { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }

        public virtual void SetModified(object entity)
        {
            Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
