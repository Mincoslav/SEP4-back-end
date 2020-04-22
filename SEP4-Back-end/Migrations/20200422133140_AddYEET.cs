using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEP4_Back_end.Migrations
{
    public partial class AddYEET : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CO2",
                columns: table => new
                {
                    CO2ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CO2_value = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2", x => x.CO2ID);
                });

            migrationBuilder.CreateTable(
                name: "Humidity",
                columns: table => new
                {
                    HUM_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HUM_value = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humidity", x => x.HUM_ID);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "Servo",
                columns: table => new
                {
                    SERV_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Spinning = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servo", x => x.SERV_ID);
                });

            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    TEMP_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEMP_value = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => x.TEMP_ID);
                });

            migrationBuilder.CreateTable(
                name: "CO2List",
                columns: table => new
                {
                    ROOM_ID = table.Column<int>(nullable: false),
                    CO2_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2List", x => new { x.CO2_ID, x.ROOM_ID });
                    table.ForeignKey(
                        name: "FK_CO2List_CO2_CO2_ID",
                        column: x => x.CO2_ID,
                        principalTable: "CO2",
                        principalColumn: "CO2ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CO2List_Room_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HumidityList",
                columns: table => new
                {
                    ROOM_ID = table.Column<int>(nullable: false),
                    HUM_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityList", x => new { x.HUM_ID, x.ROOM_ID });
                    table.ForeignKey(
                        name: "FK_HumidityList_Humidity_HUM_ID",
                        column: x => x.HUM_ID,
                        principalTable: "Humidity",
                        principalColumn: "HUM_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HumidityList_Room_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServoList",
                columns: table => new
                {
                    ROOM_ID = table.Column<int>(nullable: false),
                    SERV_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServoList", x => new { x.SERV_ID, x.ROOM_ID });
                    table.ForeignKey(
                        name: "FK_ServoList_Room_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServoList_Servo_SERV_ID",
                        column: x => x.SERV_ID,
                        principalTable: "Servo",
                        principalColumn: "SERV_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureList",
                columns: table => new
                {
                    ROOM_ID = table.Column<int>(nullable: false),
                    TEMP_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureList", x => new { x.TEMP_ID, x.ROOM_ID });
                    table.ForeignKey(
                        name: "FK_TemperatureList_Room_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemperatureList_Temperature_TEMP_ID",
                        column: x => x.TEMP_ID,
                        principalTable: "Temperature",
                        principalColumn: "TEMP_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CO2List_ROOM_ID",
                table: "CO2List",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HumidityList_ROOM_ID",
                table: "HumidityList",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ServoList_ROOM_ID",
                table: "ServoList",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureList_ROOM_ID",
                table: "TemperatureList",
                column: "ROOM_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CO2List");

            migrationBuilder.DropTable(
                name: "HumidityList");

            migrationBuilder.DropTable(
                name: "ServoList");

            migrationBuilder.DropTable(
                name: "TemperatureList");

            migrationBuilder.DropTable(
                name: "CO2");

            migrationBuilder.DropTable(
                name: "Humidity");

            migrationBuilder.DropTable(
                name: "Servo");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Temperature");
        }
    }
}
