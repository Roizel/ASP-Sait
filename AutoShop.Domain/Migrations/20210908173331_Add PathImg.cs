using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AutoShop.Domain.Migrations
{
    public partial class AddPathImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "PathImg",
               table: "AspNetUsers",
               type: "character varying(255)",
               maxLength: 255,
               nullable: true);
        }
           

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
              name: "PathImg",
              table: "AspNetUsers");
        }
    }
}
