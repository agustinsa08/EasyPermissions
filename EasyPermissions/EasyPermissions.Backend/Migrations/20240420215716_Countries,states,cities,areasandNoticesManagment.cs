using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyPermissions.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CountriesstatescitiesareasandNoticesManagment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageNotice_Notice_NoticeId",
                table: "ImageNotice");

            migrationBuilder.DropForeignKey(
                name: "FK_Notice_CategoryNotice_CategoryNoticeId",
                table: "Notice");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeNotice_CategoryNotice_CategoryNoticeId",
                table: "TypeNotice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeNotice",
                table: "TypeNotice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notice",
                table: "Notice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryNotice",
                table: "CategoryNotice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Area",
                table: "Area");

            migrationBuilder.RenameTable(
                name: "TypeNotice",
                newName: "TypeNotices");

            migrationBuilder.RenameTable(
                name: "Notice",
                newName: "Notices");

            migrationBuilder.RenameTable(
                name: "CategoryNotice",
                newName: "CategoryNotices");

            migrationBuilder.RenameTable(
                name: "Area",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_TypeNotice_CategoryNoticeId_Name",
                table: "TypeNotices",
                newName: "IX_TypeNotices_CategoryNoticeId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Notice_CategoryNoticeId_Name",
                table: "Notices",
                newName: "IX_Notices_CategoryNoticeId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryNotice_Name",
                table: "CategoryNotices",
                newName: "IX_CategoryNotices_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Area_Name",
                table: "Areas",
                newName: "IX_Areas_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeNotices",
                table: "TypeNotices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notices",
                table: "Notices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryNotices",
                table: "CategoryNotices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageNotice_Notices_NoticeId",
                table: "ImageNotice",
                column: "NoticeId",
                principalTable: "Notices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notices_CategoryNotices_CategoryNoticeId",
                table: "Notices",
                column: "CategoryNoticeId",
                principalTable: "CategoryNotices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeNotices_CategoryNotices_CategoryNoticeId",
                table: "TypeNotices",
                column: "CategoryNoticeId",
                principalTable: "CategoryNotices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageNotice_Notices_NoticeId",
                table: "ImageNotice");

            migrationBuilder.DropForeignKey(
                name: "FK_Notices_CategoryNotices_CategoryNoticeId",
                table: "Notices");

            migrationBuilder.DropForeignKey(
                name: "FK_TypeNotices_CategoryNotices_CategoryNoticeId",
                table: "TypeNotices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeNotices",
                table: "TypeNotices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notices",
                table: "Notices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryNotices",
                table: "CategoryNotices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "TypeNotices",
                newName: "TypeNotice");

            migrationBuilder.RenameTable(
                name: "Notices",
                newName: "Notice");

            migrationBuilder.RenameTable(
                name: "CategoryNotices",
                newName: "CategoryNotice");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "Area");

            migrationBuilder.RenameIndex(
                name: "IX_TypeNotices_CategoryNoticeId_Name",
                table: "TypeNotice",
                newName: "IX_TypeNotice_CategoryNoticeId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Notices_CategoryNoticeId_Name",
                table: "Notice",
                newName: "IX_Notice_CategoryNoticeId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryNotices_Name",
                table: "CategoryNotice",
                newName: "IX_CategoryNotice_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Areas_Name",
                table: "Area",
                newName: "IX_Area_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeNotice",
                table: "TypeNotice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notice",
                table: "Notice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryNotice",
                table: "CategoryNotice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Area",
                table: "Area",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageNotice_Notice_NoticeId",
                table: "ImageNotice",
                column: "NoticeId",
                principalTable: "Notice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notice_CategoryNotice_CategoryNoticeId",
                table: "Notice",
                column: "CategoryNoticeId",
                principalTable: "CategoryNotice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TypeNotice_CategoryNotice_CategoryNoticeId",
                table: "TypeNotice",
                column: "CategoryNoticeId",
                principalTable: "CategoryNotice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
