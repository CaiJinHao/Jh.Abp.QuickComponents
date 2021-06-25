using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Jh.Abp.MenuManagement.Migrations
{
    public partial class addtenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "SysMenuPermissionMap",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "SysMenuAndRoleMap",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "SysMenu",
                type: "char(36)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SysMenuPermissionMap");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SysMenuAndRoleMap");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "SysMenu");
        }
    }
}
