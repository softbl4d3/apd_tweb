using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Orders;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BusinessLogic.DBModel
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=eUseControl")
        {
        }
        public DbSet<OrderDbTable> Orders { get; set; }
        public DbSet<OrderItemDbTable> OrderItems { get; set; }
        public DbSet<DishDbTable> Dishes { get; set; }
        public DbSet<IngridientDbTable> Ingridients { get; set; }
        public DbSet<TableDbTable> Tables { get; set; }
        public DbSet<DishIngredients> DishIngredients { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
        }

    }
}
