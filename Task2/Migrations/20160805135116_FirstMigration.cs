using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task2.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AddedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Content = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 300, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UrlName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPages",
                columns: table => new
                {
                    Page1Id = table.Column<int>(nullable: false),
                    Page2Id = table.Column<int>(nullable: false),
                    RPId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPages", x => new { x.Page1Id, x.Page2Id });
                    table.ForeignKey(
                        name: "FK_RelatedPages_Pages_Page1Id",
                        column: x => x.Page1Id,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatedPages_Pages_Page2Id",
                        column: x => x.Page2Id,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NavLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    ParentLinkId = table.Column<int>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    RelatedPageId = table.Column<int>(nullable: false),
                    RelatedPagePage1Id = table.Column<int>(nullable: true),
                    RelatedPagePage2Id = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NavLinks_Pages_ParentLinkId",
                        column: x => x.ParentLinkId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NavLinks_RelatedPages_RelatedPagePage1Id_RelatedPagePage2Id",
                        columns: x => new { x.RelatedPagePage1Id, x.RelatedPagePage2Id },
                        principalTable: "RelatedPages",
                        principalColumns: new[] { "Page1Id", "Page2Id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NavLinks_ParentLinkId",
                table: "NavLinks",
                column: "ParentLinkId");

            migrationBuilder.CreateIndex(
                name: "IX_NavLinks_RelatedPagePage1Id_RelatedPagePage2Id",
                table: "NavLinks",
                columns: new[] { "RelatedPagePage1Id", "RelatedPagePage2Id" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPages_Page1Id",
                table: "RelatedPages",
                column: "Page1Id");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPages_Page2Id",
                table: "RelatedPages",
                column: "Page2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NavLinks");

            migrationBuilder.DropTable(
                name: "RelatedPages");

            migrationBuilder.DropTable(
                name: "Pages");
        }
    }
}
