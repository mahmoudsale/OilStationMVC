﻿namespace OilStationMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addactivecolumntodatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Active");
        }
    }
}