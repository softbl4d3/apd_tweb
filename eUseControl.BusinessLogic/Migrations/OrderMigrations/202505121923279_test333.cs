namespace eUseControl.BusinessLogic.Migrations.OrderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test333 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "public.OrderItems", name: "OrderDbTable_Id", newName: "OrderId_Id");
            RenameIndex(table: "public.OrderItems", name: "IX_OrderDbTable_Id", newName: "IX_OrderId_Id");
            CreateTable(
                "public.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("public.OrderItems", "DishId_Id", c => c.Int());
            AddColumn("public.Orders", "TableId_Id", c => c.Int());
            AddColumn("public.Orders", "WaiterId_Id", c => c.Int());
            CreateIndex("public.OrderItems", "DishId_Id");
            CreateIndex("public.Orders", "TableId_Id");
            CreateIndex("public.Orders", "WaiterId_Id");
            AddForeignKey("public.OrderItems", "DishId_Id", "public.Dishes", "Id");
            AddForeignKey("public.Orders", "TableId_Id", "public.Tables", "Id");
            AddForeignKey("public.Orders", "WaiterId_Id", "public.Users", "Id");
            DropColumn("public.OrderItems", "OrderId");
            DropColumn("public.OrderItems", "DishId");
            DropColumn("public.Orders", "TableId");
            DropColumn("public.Orders", "WaiterId");
        }
        
        public override void Down()
        {
            AddColumn("public.Orders", "WaiterId", c => c.Int(nullable: false));
            AddColumn("public.Orders", "TableId", c => c.Int(nullable: false));
            AddColumn("public.OrderItems", "DishId", c => c.Int(nullable: false));
            AddColumn("public.OrderItems", "OrderId", c => c.Int(nullable: false));
            DropForeignKey("public.Orders", "WaiterId_Id", "public.Users");
            DropForeignKey("public.Orders", "TableId_Id", "public.Tables");
            DropForeignKey("public.OrderItems", "DishId_Id", "public.Dishes");
            DropIndex("public.Orders", new[] { "WaiterId_Id" });
            DropIndex("public.Orders", new[] { "TableId_Id" });
            DropIndex("public.OrderItems", new[] { "DishId_Id" });
            DropColumn("public.Orders", "WaiterId_Id");
            DropColumn("public.Orders", "TableId_Id");
            DropColumn("public.OrderItems", "DishId_Id");
            DropTable("public.Users");
            RenameIndex(table: "public.OrderItems", name: "IX_OrderId_Id", newName: "IX_OrderDbTable_Id");
            RenameColumn(table: "public.OrderItems", name: "OrderId_Id", newName: "OrderDbTable_Id");
        }
    }
}
