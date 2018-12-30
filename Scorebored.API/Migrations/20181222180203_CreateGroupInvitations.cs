using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Scorebored.API.Migrations
{
    public partial class CreateGroupInvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupInvitations",
                columns: table => new
                {
                    GroupId = table.Column<long>(nullable: false),
                    RequestedById = table.Column<long>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupInvitations", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "GroupMemberships");
        }
    }
}
