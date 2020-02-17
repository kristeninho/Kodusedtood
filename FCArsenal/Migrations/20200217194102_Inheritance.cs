using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FCArsenal.Migrations
{
    public partial class Inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signing_Player_PlayerID",
                table: "Signing");

            migrationBuilder.DropIndex(name: "IX_Signing_PlayerID", table: "Signing");

            migrationBuilder.RenameTable(name: "Staff", newName: "Person");
            migrationBuilder.AddColumn<DateTime>(name: "SigningDate", table: "Person", nullable: true);
            migrationBuilder.AddColumn<string>(name: "Discriminator", table: "Person", nullable: false, maxLength: 128, defaultValue: "Staff");
            migrationBuilder.AlterColumn<DateTime>(name: "HireDate", table: "Person", nullable: true);
            migrationBuilder.AddColumn<int>(name: "OldId", table: "Person", nullable: true);

            // Copy existing Student data into new Person table.
            migrationBuilder.Sql("INSERT INTO dbo.Person (LastName, FirstName, HireDate, SigningDate, Discriminator, OldId) SELECT LastName, FirstName, null AS HireDate, SigningDate, 'Player' AS Discriminator, ID AS OldId FROM dbo.Player");
            // Fix up existing relationships to match new PK's.
            migrationBuilder.Sql("UPDATE dbo.Signing SET PlayerId = (SELECT ID FROM dbo.Person WHERE OldId = Enrollment.PlayerId AND Discriminator = 'Player')");

            // Remove temporary key
            migrationBuilder.DropColumn(name: "OldID", table: "Person");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.CreateIndex(
                 name: "IX_Signing_PlayerID",
                 table: "Signing",
                 column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Signing_Person_PlayerID",
                table: "Signing",
                column: "PlayerID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_Person_StaffID",
                table: "Department");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficeAssignment_Person_StaffID",
                table: "OfficeAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Signing_Person_PlayerID",
                table: "Signing");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingAssignment_Person_StaffID",
                table: "TrainingAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "SigningDate",
                table: "Person");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "Staff");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Staff",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    SigningDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Staff_StaffID",
                table: "Department",
                column: "StaffID",
                principalTable: "Staff",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficeAssignment_Staff_StaffID",
                table: "OfficeAssignment",
                column: "StaffID",
                principalTable: "Staff",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Signing_Player_PlayerID",
                table: "Signing",
                column: "PlayerID",
                principalTable: "Player",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingAssignment_Staff_StaffID",
                table: "TrainingAssignment",
                column: "StaffID",
                principalTable: "Staff",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
