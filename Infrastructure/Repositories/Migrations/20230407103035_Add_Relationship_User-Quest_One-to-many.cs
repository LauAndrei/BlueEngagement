using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Repositories.Migrations
{
    public partial class Add_Relationship_UserQuest_Onetomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Quests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quests_OwnerId",
                table: "Quests",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_AspNetUsers_OwnerId",
                table: "Quests",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quests_AspNetUsers_OwnerId",
                table: "Quests");

            migrationBuilder.DropIndex(
                name: "IX_Quests_OwnerId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Quests");
        }
    }
}
