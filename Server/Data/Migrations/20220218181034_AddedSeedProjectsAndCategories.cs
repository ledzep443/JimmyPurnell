using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Data.Migrations
{
    public partial class AddedSeedProjectsAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "PublishDate",
                value: "18/02/2022 06:10");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "PublishDate",
                value: "18/02/2022 06:10");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3,
                column: "PublishDate",
                value: "18/02/2022 06:10");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4,
                column: "PublishDate",
                value: "18/02/2022 06:10");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 5,
                column: "PublishDate",
                value: "18/02/2022 06:10");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 6,
                column: "PublishDate",
                value: "18/02/2022 06:10");

            migrationBuilder.InsertData(
                table: "ProjectCategories",
                columns: new[] { "ProjectCategoryId", "Description", "Name", "ThumbnailImagePath" },
                values: new object[] { 1, "A description of category 1", "Category 1", "uploads/placeholder.jpg" });

            migrationBuilder.InsertData(
                table: "ProjectCategories",
                columns: new[] { "ProjectCategoryId", "Description", "Name", "ThumbnailImagePath" },
                values: new object[] { 2, "A description of category 2", "Category 2", "uploads/placeholder.jpg" });

            migrationBuilder.InsertData(
                table: "ProjectCategories",
                columns: new[] { "ProjectCategoryId", "Description", "Name", "ThumbnailImagePath" },
                values: new object[] { 3, "A description of category 3", "Category 3", "uploads/placeholder.jpg" });

            migrationBuilder.InsertData(
                table: "ProjectCategories",
                columns: new[] { "ProjectCategoryId", "Description", "Name", "ThumbnailImagePath" },
                values: new object[] { 4, "A description of category 4", "Category 4", "uploads/placeholder.jpg" });

            migrationBuilder.InsertData(
                table: "ProjectCategories",
                columns: new[] { "ProjectCategoryId", "Description", "Name", "ThumbnailImagePath" },
                values: new object[] { 5, "A description of category 5", "Category 5", "uploads/placeholder.jpg" });

            migrationBuilder.InsertData(
                table: "ProjectCategories",
                columns: new[] { "ProjectCategoryId", "Description", "Name", "ThumbnailImagePath" },
                values: new object[] { 6, "A description of category 6", "Category 6", "uploads/placeholder.jpg" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "GitHub", "IsPublished", "Name", "ProjectCategoryId", "PublishDate", "ScreenshotImagePath", "URL" },
                values: new object[] { 1, "This is the description for project 1", "/", false, "First project", 1, "18/02/2022 06:10", "uploads/placeholder.jpg", "/" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "GitHub", "IsPublished", "Name", "ProjectCategoryId", "PublishDate", "ScreenshotImagePath", "URL" },
                values: new object[] { 2, "This is the description for project 2", "/", false, "Second project", 2, "18/02/2022 06:10", "uploads/placeholder.jpg", "/" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "GitHub", "IsPublished", "Name", "ProjectCategoryId", "PublishDate", "ScreenshotImagePath", "URL" },
                values: new object[] { 3, "This is the description for project 3", "/", false, "Thrid project", 3, "18/02/2022 06:10", "uploads/placeholder.jpg", "/" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "GitHub", "IsPublished", "Name", "ProjectCategoryId", "PublishDate", "ScreenshotImagePath", "URL" },
                values: new object[] { 4, "This is the description for project 4", "/", false, "Fourth project", 4, "18/02/2022 06:10", "uploads/placeholder.jpg", "/" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "GitHub", "IsPublished", "Name", "ProjectCategoryId", "PublishDate", "ScreenshotImagePath", "URL" },
                values: new object[] { 5, "This is the description for project 5", "/", false, "Fifth project", 5, "18/02/2022 06:10", "uploads/placeholder.jpg", "/" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "GitHub", "IsPublished", "Name", "ProjectCategoryId", "PublishDate", "ScreenshotImagePath", "URL" },
                values: new object[] { 6, "This is the description for project 6", "/", false, "Sixth project", 6, "18/02/2022 06:10", "uploads/placeholder.jpg", "/" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProjectCategories",
                keyColumn: "ProjectCategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProjectCategories",
                keyColumn: "ProjectCategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProjectCategories",
                keyColumn: "ProjectCategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProjectCategories",
                keyColumn: "ProjectCategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProjectCategories",
                keyColumn: "ProjectCategoryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProjectCategories",
                keyColumn: "ProjectCategoryId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 1,
                column: "PublishDate",
                value: "17/02/2022 03:11");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 2,
                column: "PublishDate",
                value: "17/02/2022 03:11");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 3,
                column: "PublishDate",
                value: "17/02/2022 03:11");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 4,
                column: "PublishDate",
                value: "17/02/2022 03:11");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 5,
                column: "PublishDate",
                value: "17/02/2022 03:11");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "PostId",
                keyValue: 6,
                column: "PublishDate",
                value: "17/02/2022 03:11");
        }
    }
}
