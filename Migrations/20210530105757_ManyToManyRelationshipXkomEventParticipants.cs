using Microsoft.EntityFrameworkCore.Migrations;

namespace xkomEventsAPI.Migrations
{
    public partial class ManyToManyRelationshipXkomEventParticipants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Events_XkomEventId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_XkomEventId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "XkomEventId",
                table: "Participants");

            migrationBuilder.CreateTable(
                name: "ParticipantsToXkomEvents",
                columns: table => new
                {
                    ParticipantToXkomEventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    XkomEventId = table.Column<int>(type: "int", nullable: false),
                    ParticipantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantsToXkomEvents", x => x.ParticipantToXkomEventId);
                    table.ForeignKey(
                        name: "FK_ParticipantsToXkomEvents_Events_XkomEventId",
                        column: x => x.XkomEventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantsToXkomEvents_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsToXkomEvents_ParticipantId",
                table: "ParticipantsToXkomEvents",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantsToXkomEvents_XkomEventId",
                table: "ParticipantsToXkomEvents",
                column: "XkomEventId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantsToXkomEvents");

            migrationBuilder.AddColumn<int>(
                name: "XkomEventId",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_XkomEventId",
                table: "Participants",
                column: "XkomEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_XkomEventId",
                table: "Participants",
                column: "XkomEventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
