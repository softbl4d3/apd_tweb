namespace eUseControl.BusinessLogic.Migrations.OrderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.IngridientDbTableDishDbTables", "IngridientDbTable_Id", "public.Ingridients");
            DropForeignKey("public.IngridientDbTableDishDbTables", "DishDbTable_Id", "public.Dishes");
            DropIndex("public.IngridientDbTableDishDbTables", new[] { "IngridientDbTable_Id" });
            DropIndex("public.IngridientDbTableDishDbTables", new[] { "DishDbTable_Id" });
            DropTable("public.IngridientDbTableDishDbTables");
        }
        
        public override void Down()
        {
            CreateTable(
                "public.IngridientDbTableDishDbTables",
                c => new
                    {
                        IngridientDbTable_Id = c.Int(nullable: false),
                        DishDbTable_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IngridientDbTable_Id, t.DishDbTable_Id });
            
            CreateIndex("public.IngridientDbTableDishDbTables", "DishDbTable_Id");
            CreateIndex("public.IngridientDbTableDishDbTables", "IngridientDbTable_Id");
            AddForeignKey("public.IngridientDbTableDishDbTables", "DishDbTable_Id", "public.Dishes", "Id", cascadeDelete: true);
            AddForeignKey("public.IngridientDbTableDishDbTables", "IngridientDbTable_Id", "public.Ingridients", "Id", cascadeDelete: true);
        }
    }
}
