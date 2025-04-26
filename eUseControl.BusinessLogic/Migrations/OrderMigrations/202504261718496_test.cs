namespace eUseControl.BusinessLogic.Migrations.OrderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("public.Ingridients", "DishDbTable_Id", "public.Dishes");
            DropIndex("public.Ingridients", new[] { "DishDbTable_Id" });
            CreateTable(
                "public.IngridientDbTableDishDbTables",
                c => new
                    {
                        IngridientDbTable_Id = c.Int(nullable: false),
                        DishDbTable_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IngridientDbTable_Id, t.DishDbTable_Id })
                .ForeignKey("public.Ingridients", t => t.IngridientDbTable_Id, cascadeDelete: true)
                .ForeignKey("public.Dishes", t => t.DishDbTable_Id, cascadeDelete: true)
                .Index(t => t.IngridientDbTable_Id)
                .Index(t => t.DishDbTable_Id);
            
            DropColumn("public.Ingridients", "DishDbTable_Id");
        }
        
        public override void Down()
        {
            AddColumn("public.Ingridients", "DishDbTable_Id", c => c.Int());
            DropForeignKey("public.IngridientDbTableDishDbTables", "DishDbTable_Id", "public.Dishes");
            DropForeignKey("public.IngridientDbTableDishDbTables", "IngridientDbTable_Id", "public.Ingridients");
            DropIndex("public.IngridientDbTableDishDbTables", new[] { "DishDbTable_Id" });
            DropIndex("public.IngridientDbTableDishDbTables", new[] { "IngridientDbTable_Id" });
            DropTable("public.IngridientDbTableDishDbTables");
            CreateIndex("public.Ingridients", "DishDbTable_Id");
            AddForeignKey("public.Ingridients", "DishDbTable_Id", "public.Dishes", "Id");
        }
    }
}
