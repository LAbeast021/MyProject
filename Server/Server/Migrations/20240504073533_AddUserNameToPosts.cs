using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class AddUserNameToPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add a new column 'UserName' to the 'Post' table
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the 'UserName' column from the 'Post' table if rolling back this migration
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Post"
            );
        }
    }
}