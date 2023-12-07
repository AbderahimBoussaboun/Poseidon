using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeFirstExistingDatabaseSample.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balancers",
                columns: table => new
                {
                    BalancerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ports = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balancers", x => x.BalancerId);
                });

            migrationBuilder.CreateTable(
                name: "ComponetTypes",
                columns: table => new
                {
                    ComponentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponetTypes", x => x.ComponentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Environments",
                columns: table => new
                {
                    EnvironmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Environments", x => x.EnvironmentId);
                });

            migrationBuilder.CreateTable(
                name: "Infraestructures",
                columns: table => new
                {
                    InfraestructureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infraestructures", x => x.InfraestructureId);
                });

            migrationBuilder.CreateTable(
                name: "Monitors",
                columns: table => new
                {
                    MonitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
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
                    NodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
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
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Pools",
                columns: table => new
                {
                    PoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MonitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BalancerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvironmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.ServerId);
                    table.ForeignKey(
                        name: "FK_Servers_Environments_EnvironmentId",
                        column: x => x.EnvironmentId,
                        principalTable: "Environments",
                        principalColumn: "EnvironmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Virtuals",
                columns: table => new
                {
                    VirtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Virtuals", x => x.VirtualId);
                    table.ForeignKey(
                        name: "FK_Virtuals_Pools_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "PoolId");
                });

            migrationBuilder.CreateTable(
                name: "SubApplications",
                columns: table => new
                {
                    SubApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubApplications", x => x.SubApplicationId);
                    table.ForeignKey(
                        name: "FK_SubApplications_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "ServerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VirtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_Virtuals_VirtualId",
                        column: x => x.VirtualId,
                        principalTable: "Virtuals",
                        principalColumn: "VirtualId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ports = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BalancerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentId);
                    table.ForeignKey(
                        name: "FK_Components_Balancers_BalancerId",
                        column: x => x.BalancerId,
                        principalTable: "Balancers",
                        principalColumn: "BalancerId");
                    table.ForeignKey(
                        name: "FK_Components_ComponetTypes_ComponentTypeId",
                        column: x => x.ComponentTypeId,
                        principalTable: "ComponetTypes",
                        principalColumn: "ComponentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_SubApplications_SubApplicationId",
                        column: x => x.SubApplicationId,
                        principalTable: "SubApplications",
                        principalColumn: "SubApplicationId");
                });

            migrationBuilder.CreateTable(
                name: "Irules",
                columns: table => new
                {
                    IruleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Redirect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irules", x => x.IruleId);
                    table.ForeignKey(
                        name: "FK_Irules_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "RuleId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServerApplications",
                columns: table => new
                {
                    ServerApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateInsert = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDisable = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerApplications", x => x.ServerApplicationId);
                    table.ForeignKey(
                        name: "FK_ServerApplications_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_ServerApplications_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ProductId",
                table: "Applications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_BalancerId",
                table: "Components",
                column: "BalancerId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypeId",
                table: "Components",
                column: "ComponentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_SubApplicationId",
                table: "Components",
                column: "SubApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Irules_RuleId",
                table: "Irules",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_NodePool_PoolId",
                table: "NodePool",
                column: "PoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Pools_MonitorId",
                table: "Pools",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ServerId",
                table: "Roles",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_VirtualId",
                table: "Rules",
                column: "VirtualId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerApplications_ComponentId",
                table: "ServerApplications",
                column: "ComponentId",
                unique: true,
                filter: "([ComponentId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_ServerApplications_RoleId",
                table: "ServerApplications",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_EnvironmentId",
                table: "Servers",
                column: "EnvironmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_ProductId",
                table: "Servers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubApplications_ApplicationId",
                table: "SubApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Virtuals_PoolId",
                table: "Virtuals",
                column: "PoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Infraestructures");

            migrationBuilder.DropTable(
                name: "Irules");

            migrationBuilder.DropTable(
                name: "NodePool");

            migrationBuilder.DropTable(
                name: "ServerApplications");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Nodes");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Virtuals");

            migrationBuilder.DropTable(
                name: "Balancers");

            migrationBuilder.DropTable(
                name: "ComponetTypes");

            migrationBuilder.DropTable(
                name: "SubApplications");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Environments");

            migrationBuilder.DropTable(
                name: "Monitors");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
