using Microsoft.EntityFrameworkCore.Migrations;

namespace WhereToGoWebApi.Migrations
{
    public partial class InviteFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventMeeting_Meeting_MeetingId",
                table: "EventMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_EventMeeting_AspNetUsers_UserId",
                table: "EventMeeting");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_Events_EventId",
                table: "Meeting");

            migrationBuilder.DropForeignKey(
                name: "FK_Meeting_AspNetUsers_UserId",
                table: "Meeting");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_Events_EventId",
                table: "UserEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvent_AspNetUsers_UserId",
                table: "UserEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventMeeting",
                table: "EventMeeting");

            migrationBuilder.RenameTable(
                name: "UserEvent",
                newName: "UserEvents");

            migrationBuilder.RenameTable(
                name: "Meeting",
                newName: "Meetings");

            migrationBuilder.RenameTable(
                name: "EventMeeting",
                newName: "EventMeetings");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvent_UserId",
                table: "UserEvents",
                newName: "IX_UserEvents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvent_EventId",
                table: "UserEvents",
                newName: "IX_UserEvents_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_UserId",
                table: "Meetings",
                newName: "IX_Meetings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meeting_EventId",
                table: "Meetings",
                newName: "IX_Meetings_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventMeeting_UserId",
                table: "EventMeetings",
                newName: "IX_EventMeetings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventMeeting_MeetingId",
                table: "EventMeetings",
                newName: "IX_EventMeetings_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents",
                column: "UserEventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings",
                column: "MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventMeetings",
                table: "EventMeetings",
                column: "EventMeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventMeetings_Meetings_MeetingId",
                table: "EventMeetings",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "MeetingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventMeetings_AspNetUsers_UserId",
                table: "EventMeetings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Events_EventId",
                table: "Meetings",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_UserId",
                table: "Meetings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_AspNetUsers_UserId",
                table: "UserEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventMeetings_Meetings_MeetingId",
                table: "EventMeetings");

            migrationBuilder.DropForeignKey(
                name: "FK_EventMeetings_AspNetUsers_UserId",
                table: "EventMeetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Events_EventId",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_UserId",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_AspNetUsers_UserId",
                table: "UserEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventMeetings",
                table: "EventMeetings");

            migrationBuilder.RenameTable(
                name: "UserEvents",
                newName: "UserEvent");

            migrationBuilder.RenameTable(
                name: "Meetings",
                newName: "Meeting");

            migrationBuilder.RenameTable(
                name: "EventMeetings",
                newName: "EventMeeting");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvents_UserId",
                table: "UserEvent",
                newName: "IX_UserEvent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvent",
                newName: "IX_UserEvent_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_UserId",
                table: "Meeting",
                newName: "IX_Meeting_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_EventId",
                table: "Meeting",
                newName: "IX_Meeting_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventMeetings_UserId",
                table: "EventMeeting",
                newName: "IX_EventMeeting_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_EventMeetings_MeetingId",
                table: "EventMeeting",
                newName: "IX_EventMeeting_MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent",
                column: "UserEventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meeting",
                table: "Meeting",
                column: "MeetingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventMeeting",
                table: "EventMeeting",
                column: "EventMeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventMeeting_Meeting_MeetingId",
                table: "EventMeeting",
                column: "MeetingId",
                principalTable: "Meeting",
                principalColumn: "MeetingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventMeeting_AspNetUsers_UserId",
                table: "EventMeeting",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_Events_EventId",
                table: "Meeting",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meeting_AspNetUsers_UserId",
                table: "Meeting",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_Events_EventId",
                table: "UserEvent",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvent_AspNetUsers_UserId",
                table: "UserEvent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
