namespace BANGTANS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
 
            CreateTable(
                "dbo.AlbumViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Subtitle = c.String(),
                        ReleasedDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        ArtistViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArtistViewModels", t => t.ArtistViewModel_Id)
                .ForeignKey("dbo.ArtistViewModels", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.ArtistViewModel_Id);
            
            CreateTable(
                "dbo.ArtistViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DebutDate = c.DateTime(nullable: false),
                        Description = c.String(nullable: false),
                        AlbumViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumViewModels", t => t.AlbumViewModel_Id)
                .Index(t => t.AlbumViewModel_Id);
            
            CreateTable(
                "dbo.MemberViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StageName = c.String(),
                        Name = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        BirtyDay = c.DateTime(nullable: false),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArtistViewModels", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ArtistViewModels", "AlbumViewModel_Id", "dbo.AlbumViewModels");
            DropForeignKey("dbo.AlbumViewModels", "ArtistId", "dbo.ArtistViewModels");
            DropForeignKey("dbo.MemberViewModels", "ArtistId", "dbo.ArtistViewModels");
            DropForeignKey("dbo.AlbumViewModels", "ArtistViewModel_Id", "dbo.ArtistViewModels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MemberViewModels", new[] { "ArtistId" });
            DropIndex("dbo.ArtistViewModels", new[] { "AlbumViewModel_Id" });
            DropIndex("dbo.AlbumViewModels", new[] { "ArtistViewModel_Id" });
            DropIndex("dbo.AlbumViewModels", new[] { "ArtistId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MemberViewModels");
            DropTable("dbo.ArtistViewModels");
            DropTable("dbo.AlbumViewModels");
        }
    }
}
