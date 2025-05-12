using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteToComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_LostPets_PetId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_LostPets_PetId",
                table: "Comments",
                column: "PetId",
                principalTable: "LostPets",
                principalColumn: "PetId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_LostPets_PetId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_LostPets_PetId",
                table: "Comments",
                column: "PetId",
                principalTable: "LostPets",
                principalColumn: "PetId");
        }
    }
}
