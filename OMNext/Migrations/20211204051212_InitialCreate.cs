using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OMNext.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrator",
                columns: table => new
                {
                    AdministratorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 25, nullable: false),
                    LastName = table.Column<string>(maxLength: 25, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrator", x => x.AdministratorID);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ChatID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionID = table.Column<int>(nullable: false),
                    From = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 8000, nullable: false),
                    SentDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatID);
                });

            migrationBuilder.CreateTable(
                name: "CLCCenter",
                columns: table => new
                {
                    CLCCenterID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(maxLength: 10, nullable: false),
                    CenterName = table.Column<string>(maxLength: 50, nullable: false),
                    Address1 = table.Column<string>(maxLength: 100, nullable: false),
                    Address2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    Zipcode = table.Column<string>(maxLength: 15, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    HasAccessToMedComm = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLCCenter", x => x.CLCCenterID);
                });

            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    ConnectionID = table.Column<string>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true),
                    Connected = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.ConnectionID);
                });

            migrationBuilder.CreateTable(
                name: "DataDrive",
                columns: table => new
                {
                    DataDriveID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionID = table.Column<int>(nullable: false),
                    TeamID = table.Column<int>(nullable: false),
                    CurrentRecord = table.Column<int>(nullable: false),
                    CurrentTimerInterval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDrive", x => x.DataDriveID);
                });

            migrationBuilder.CreateTable(
                name: "DataDriveData",
                columns: table => new
                {
                    DataDriveDataID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissionID = table.Column<int>(nullable: false),
                    From = table.Column<int>(nullable: false),
                    To = table.Column<int>(nullable: false),
                    DataInput = table.Column<string>(maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataDriveData", x => x.DataDriveDataID);
                });

            migrationBuilder.CreateTable(
                name: "Script",
                columns: table => new
                {
                    ScriptID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    ScriptBody = table.Column<string>(maxLength: 2147483647, nullable: false),
                    CreatedOnDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Script", x => x.ScriptID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<int>(nullable: false),
                    Password = table.Column<string>(maxLength: 25, nullable: false),
                    MissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "RunningMission",
                columns: table => new
                {
                    MissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightDirector = table.Column<string>(maxLength: 50, nullable: false),
                    Booth = table.Column<string>(maxLength: 10, nullable: false),
                    MissionEnding = table.Column<int>(nullable: false),
                    MissionDate = table.Column<DateTime>(nullable: true),
                    ScriptID = table.Column<int>(nullable: true),
                    CLCCenterID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunningMission", x => x.MissionID);
                    table.ForeignKey(
                        name: "FK_RunningMission_CLCCenter_CLCCenterID",
                        column: x => x.CLCCenterID,
                        principalTable: "CLCCenter",
                        principalColumn: "CLCCenterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RunningMission_CLCCenterID",
                table: "RunningMission",
                column: "CLCCenterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrator");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Connection");

            migrationBuilder.DropTable(
                name: "DataDrive");

            migrationBuilder.DropTable(
                name: "DataDriveData");

            migrationBuilder.DropTable(
                name: "RunningMission");

            migrationBuilder.DropTable(
                name: "Script");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "CLCCenter");
        }
    }
}
