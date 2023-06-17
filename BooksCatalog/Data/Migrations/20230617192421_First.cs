using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksCatalog.Data.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookModels_AuthorModels_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AuthorModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookModels_AuthorId",
                table: "BookModels",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookModels");

            migrationBuilder.DropTable(
                name: "AuthorModels");
        }
    }
}
