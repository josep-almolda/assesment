using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Notifications.DataAccess.Migrations
{
    public partial class NotificationsDataAccessNotificationsDbContextSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Body", "EventType", "Title" },
                values: new object[] { new Guid("d92af9fe-a1e7-4534-938c-5b3067edf3e0"), "Hi {Firstname}, your appointment with {OrganisationName} at {AppointmentDateTime} has been - cancelled for the following reason: {Reason}.", "AppointmentCancelled", "Appointment Cancelled" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: new Guid("d92af9fe-a1e7-4534-938c-5b3067edf3e0"));
        }
    }
}
