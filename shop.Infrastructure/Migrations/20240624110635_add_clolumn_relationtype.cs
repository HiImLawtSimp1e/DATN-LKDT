using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_clolumn_relationtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelationType",
                table: "ItemObjRelationEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelationType",
                table: "ItemObjRelationEntities");
        }
    }
}
