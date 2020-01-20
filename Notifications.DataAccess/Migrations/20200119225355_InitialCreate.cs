using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notifications.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    EventType = table.Column<string>(maxLength: 250, nullable: true),
                    Message = table.Column<string>(maxLength: 1000, nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(maxLength: 1000, nullable: true),
                    Title = table.Column<string>(maxLength: 250, nullable: true),
                    EventType = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Notification",
                columns: new[] { "Id", "DateAdded", "EventType", "Message", "UserId" },
                values: new object[,]
                {
                    { new Guid("5f46e0cd-c3ef-4d81-8646-0b612ee93f99"), new DateTime(2020, 1, 17, 22, 53, 55, 287, DateTimeKind.Local), "AppointmentCancelled", "1 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.", new Guid("ae60bbba-ac45-41e8-bb93-2e3db395b114") },
                    { new Guid("561670a1-76db-4e8e-accf-5a031437f253"), new DateTime(2020, 1, 18, 22, 53, 55, 290, DateTimeKind.Local), "AppointmentCancelled", "2 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.", new Guid("ae60bbba-ac45-41e8-bb93-2e3db395b114") },
                    { new Guid("3dd4ed21-ce69-457b-885a-bc4fdbe407d6"), new DateTime(2020, 1, 19, 22, 53, 55, 290, DateTimeKind.Local), "AppointmentCancelled", "3 Hi Firstname, your appointment with OrganisationName at AppointmentDateTime has been cancelled for the following reason: Reason.", new Guid("ae60bbba-ac45-41e8-bb93-2e3db395b114") }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "Template",
                columns: new[] { "Id", "Body", "EventType", "Title" },
                values: new object[] { new Guid("e0c77fe8-53e3-4473-8dcb-896e340d06a1"), "Hi {Firstname}, your appointment with {OrganisationName} at {AppointmentDateTime} has been cancelled for the following reason: {Reason}.", "AppointmentCancelled", "Appointment Cancelled" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Template",
                schema: "core");
        }
    }
}
