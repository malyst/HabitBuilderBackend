using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitBuilder_Backend.Migrations
{
    public partial class fixuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHabits_AspNetUsers_AppUserId",
                table: "UserHabits");

            migrationBuilder.DropIndex(
                name: "IX_UserHabits_AppUserId",
                table: "UserHabits");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserHabits");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserHabits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserHabits");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "UserHabits",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserHabits_AppUserId",
                table: "UserHabits",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHabits_AspNetUsers_AppUserId",
                table: "UserHabits",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
