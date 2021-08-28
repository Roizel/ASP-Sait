using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AutoShop.Domain.Migrations
{
    public partial class AddPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathImg",
                table: "tblCars",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "tbluser",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            //        userpassword = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            //        useremail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tbluser", x => x.id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbluser");

            migrationBuilder.DropColumn(
                name: "PathImg",
                table: "tblCars");
        }
    }
}
