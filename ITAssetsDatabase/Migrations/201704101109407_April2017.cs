namespace ITAssetsDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class April2017 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItem", "EndUserId", "dbo.EndUser");
            DropForeignKey("dbo.Item", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.Item", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.ProgressAudit", "OrderId", "dbo.Order");
            DropForeignKey("dbo.ProgressAudit", "OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.Order", "RaisedById", "dbo.RaisedBy");
            DropForeignKey("dbo.Asset", "HostnameXafinityId", "dbo.HostnameXafinity");
            DropForeignKey("dbo.Order", "StaffId", "dbo.Staff");
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.OrderItem", new[] { "EndUserId" });
            DropIndex("dbo.Item", new[] { "OrderItemId" });
            DropIndex("dbo.Item", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "RaisedById" });
            DropIndex("dbo.Order", new[] { "StaffId" });
            DropIndex("dbo.ProgressAudit", new[] { "OrderId" });
            DropIndex("dbo.ProgressAudit", new[] { "OrderStatusId" });
            DropIndex("dbo.Asset", new[] { "HostnameXafinityId" });
            DropColumn("dbo.Asset", "HostnameXafinityId");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Item");
            DropTable("dbo.Order");
            DropTable("dbo.ProgressAudit");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.HostnameXafinity");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HostnameXafinity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        FullHostname = c.String(),
                        Location = c.String(),
                        StaffId = c.Int(nullable: false),
                        EndUserId = c.String(),
                        ProductTypeId = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProgressAudit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        OrderId = c.Int(nullable: false),
                        OrderStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Notes = c.String(),
                        HDRef = c.String(),
                        PRRef = c.String(),
                        PORef = c.String(),
                        SupplierRef = c.String(),
                        CourierRef = c.String(),
                        CostCentreOveride = c.String(),
                        ProjectCodeOveride = c.String(),
                        QuoteAttachment = c.String(),
                        RaisedById = c.Int(nullable: false),
                        StaffId = c.Int(nullable: false),
                        OrderConfirmed = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Barcode = c.String(),
                        SerialNo = c.String(),
                        Hostname = c.String(),
                        Quantity = c.Int(nullable: false),
                        OrderItemId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        EndUserId = c.Int(nullable: false),
                        EndUserSID = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Asset", "HostnameXafinityId", c => c.Int());
            CreateIndex("dbo.Asset", "HostnameXafinityId");
            CreateIndex("dbo.ProgressAudit", "OrderStatusId");
            CreateIndex("dbo.ProgressAudit", "OrderId");
            CreateIndex("dbo.Order", "StaffId");
            CreateIndex("dbo.Order", "RaisedById");
            CreateIndex("dbo.Item", "ProductId");
            CreateIndex("dbo.Item", "OrderItemId");
            CreateIndex("dbo.OrderItem", "EndUserId");
            CreateIndex("dbo.OrderItem", "OrderId");
            AddForeignKey("dbo.Order", "StaffId", "dbo.Staff", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Asset", "HostnameXafinityId", "dbo.HostnameXafinity", "Id");
            AddForeignKey("dbo.Order", "RaisedById", "dbo.RaisedBy", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProgressAudit", "OrderStatusId", "dbo.OrderStatus", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProgressAudit", "OrderId", "dbo.Order", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItem", "OrderId", "dbo.Order", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Item", "ProductId", "dbo.Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Item", "OrderItemId", "dbo.OrderItem", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderItem", "EndUserId", "dbo.EndUser", "Id", cascadeDelete: true);
        }
    }
}
