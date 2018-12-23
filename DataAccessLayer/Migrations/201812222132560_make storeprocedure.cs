namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makestoreprocedure : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Item_Insert",
                p => new
                    {
                        BarCode = p.String(maxLength: 150),
                        Name = p.String(maxLength: 200),
                        Price = p.Decimal(precision: 9, scale: 2),
                    },
                body:
                    @"INSERT [dbo].[Items]([BarCode], [Name], [Price])
                      VALUES (@BarCode, @Name, @Price)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Items]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Items] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Item_Update",
                p => new
                    {
                        Id = p.Int(),
                        BarCode = p.String(maxLength: 150),
                        Name = p.String(maxLength: 200),
                        Price = p.Decimal(precision: 9, scale: 2),
                    },
                body:
                    @"UPDATE [dbo].[Items]
                      SET [BarCode] = @BarCode, [Name] = @Name, [Price] = @Price
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Item_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Items]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Item_Delete");
            DropStoredProcedure("dbo.Item_Update");
            DropStoredProcedure("dbo.Item_Insert");
        }
    }
}
