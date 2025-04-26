namespace eUseControl.BusinessLogic.Migrations.OrderMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<eUseControl.BusinessLogic.DBModel.OrderContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations\OrderMigrations";
            ContextKey = "eUseControl.BusinessLogic.DBModel.OrderContext";
        }

        protected override void Seed(eUseControl.BusinessLogic.DBModel.OrderContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
