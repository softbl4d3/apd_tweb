namespace eUseControl.BusinessLogic.Migrations.OrderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.DishIngredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Dish_Id = c.Int(),
                        Ingredient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Dishes", t => t.Dish_Id)
                .ForeignKey("public.Ingridients", t => t.Ingredient_Id)
                .Index(t => t.Dish_Id)
                .Index(t => t.Ingredient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.DishIngredients", "Ingredient_Id", "public.Ingridients");
            DropForeignKey("public.DishIngredients", "Dish_Id", "public.Dishes");
            DropIndex("public.DishIngredients", new[] { "Ingredient_Id" });
            DropIndex("public.DishIngredients", new[] { "Dish_Id" });
            DropTable("public.DishIngredients");
        }
    }
}
