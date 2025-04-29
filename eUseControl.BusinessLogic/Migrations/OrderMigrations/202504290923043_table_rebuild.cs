namespace eUseControl.BusinessLogic.Migrations.OrderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class table_rebuild : DbMigration
    {
        public override void Up()
        {
            DropColumn("public.Tables", "ReservationTime");
        }
        
        public override void Down()
        {
            AddColumn("public.Tables", "ReservationTime", c => c.DateTime());
        }
    }
}
