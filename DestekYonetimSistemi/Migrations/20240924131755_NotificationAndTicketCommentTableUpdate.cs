using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DestekYonetimSistemi.Migrations
{
    /// <inheritdoc />
    public partial class NotificationAndTicketCommentTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TicketComments");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "TicketComments",
                newName: "SupportTicketId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TicketComments_SupportTicketId",
                table: "TicketComments",
                column: "SupportTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_SupportTickets_SupportTicketId",
                table: "TicketComments",
                column: "SupportTicketId",
                principalTable: "SupportTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_SupportTickets_SupportTicketId",
                table: "TicketComments");

            migrationBuilder.DropIndex(
                name: "IX_TicketComments_SupportTicketId",
                table: "TicketComments");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "SupportTicketId",
                table: "TicketComments",
                newName: "TicketId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TicketComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Notifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
