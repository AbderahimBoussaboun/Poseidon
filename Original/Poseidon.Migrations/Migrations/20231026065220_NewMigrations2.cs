using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Poseidon.Migrations.Migrations
{
    public partial class NewMigrations2 : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Irules");

            migrationBuilder.DropTable(
                name: "NodePool");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropTable(
                name: "Monitors");

            migrationBuilder.DropTable(
                name: "Virtuals");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monitors",
                columns: table => new
                {
                    MonitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Adaptive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cipherlist = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Compatibility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Debug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Defaults_from = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP_DSCP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Interval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RECV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RECV_disable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reverse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SEND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Server = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    get = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ssl_profile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time_until_up = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timeout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.MonitorId);
                });

            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    NodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.NodeId);
                });

            migrationBuilder.CreateTable(
                name: "Virtuals",
                columns: table => new
                {
                    VirtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Virtuals", x => x.VirtualId);
                });

            migrationBuilder.CreateTable(
                name: "Pools",
                columns: table => new
                {
                    PoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MonitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BalancerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VirtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pools", x => x.PoolId);
                    table.ForeignKey(
                        name: "FK_Pools_Monitors_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "Monitors",
                        principalColumn: "MonitorId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Pools_Virtuals_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Virtuals",
                        principalColumn: "VirtualId");
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VirtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VirtualId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_Virtuals_VirtualId1",
                        column: x => x.VirtualId1,
                        principalTable: "Virtuals",
                        principalColumn: "VirtualId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "NodePool",
                columns: table => new
                {
                    NodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VirtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodePool", x => new { x.NodeId, x.PoolId });
                    table.ForeignKey(
                        name: "FK_NodePool_Nodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Nodes",
                        principalColumn: "NodeId");
                    table.ForeignKey(
                        name: "FK_NodePool_Pools_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "PoolId");
                });

            migrationBuilder.CreateTable(
                name: "Irules",
                columns: table => new
                {
                    IruleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Redirect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irules", x => x.IruleId);
                    table.ForeignKey(
                        name: "FK_Irules_Rules_RuleId1",
                        column: x => x.RuleId1,
                        principalTable: "Rules",
                        principalColumn: "RuleId",
                        onDelete: ReferentialAction.SetNull);
                });
        }
    }
}
