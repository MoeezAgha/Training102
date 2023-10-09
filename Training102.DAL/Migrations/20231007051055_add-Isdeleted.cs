using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training102.DAL.Migrations
{
    public partial class addIsdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TrainingAssignToUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TrainingAssignToUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Training",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Training",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "QuizCompletion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "QuizCompletion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Quiz",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Quiz",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Question",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Question",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Answer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Answer",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TrainingAssignToUser");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TrainingAssignToUser");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Training");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "QuizCompletion");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "QuizCompletion");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Answer");
        }
    }
}
