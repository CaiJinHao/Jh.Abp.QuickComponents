using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jh.Abp.MenuManagement.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "VARCHAR(36)", nullable: false),
                    Code = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: false, comment: "菜单编号"),
                    Name = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false, comment: "菜单名称"),
                    Icon = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false, comment: "菜单图标"),
                    Sort = table.Column<int>(type: "INT", nullable: false, comment: "同一级别内排序"),
                    ParentCode = table.Column<string>(type: "NVARCHAR2(64)", maxLength: 64, nullable: true, comment: "上级菜单编号"),
                    Url = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true, comment: "导航路径"),
                    Description = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true, comment: "菜单描述"),
                    ExtraProperties = table.Column<string>(type: "NVARCHAR2(8188)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CreatorId = table.Column<Guid>(type: "VARCHAR(36)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "VARCHAR(36)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "VARCHAR(36)", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenu", x => x.Id);
                },
                comment: "菜单");

            migrationBuilder.CreateTable(
                name: "SysMenuAndRoleMap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "VARCHAR(36)", nullable: false),
                    MenuId = table.Column<Guid>(type: "VARCHAR(36)", nullable: false),
                    RoleId = table.Column<Guid>(type: "VARCHAR(36)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    CreatorId = table.Column<Guid>(type: "VARCHAR(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysMenuAndRoleMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysMenuAndRoleMap_SysMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "SysMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "菜单和角色映射表");

            migrationBuilder.CreateIndex(
                name: "IX_SysMenuAndRoleMap_MenuId",
                table: "SysMenuAndRoleMap",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_SysMenuAndRoleMap_RoleId",
                table: "SysMenuAndRoleMap",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysMenuAndRoleMap");

            migrationBuilder.DropTable(
                name: "SysMenu");
        }
    }
}
