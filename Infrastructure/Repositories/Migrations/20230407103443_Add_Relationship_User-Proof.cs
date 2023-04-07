using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Repositories.Migrations
{
    public partial class Add_Relationship_UserProof : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Proofs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proofs_OwnerId",
                table: "Proofs",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proofs_AspNetUsers_OwnerId",
                table: "Proofs",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proofs_AspNetUsers_OwnerId",
                table: "Proofs");

            migrationBuilder.DropIndex(
                name: "IX_Proofs_OwnerId",
                table: "Proofs");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Proofs");
        }
    }
}
