using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApi.Migrations
{
    /// <inheritdoc />
    public partial class WithoutSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HighestEducation = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recruiters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredEducation = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecruiterId = table.Column<int>(type: "int", nullable: false),
                    HiredCandidateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_Candidates_HiredCandidateId",
                        column: x => x.HiredCandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Recruiters_RecruiterId",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobCandidateMatches",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCandidateMatches", x => new { x.JobId, x.CandidateId });
                    table.ForeignKey(
                        name: "FK_JobCandidateMatches_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobCandidateMatches_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "HighestEducation", "Name", "Status" },
                values: new object[,]
                {
                    { 1, 2, "Angel Hernandez", "Active" },
                    { 2, 1, "Ryan Smith", "Active" },
                    { 3, 3, "Courtney Bush", "Active" },
                    { 4, 2, "Lorenzo White", "Active" }
                });

            migrationBuilder.InsertData(
                table: "Recruiters",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Susan Berkly" },
                    { 2, "Tom Holms" }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "HiredCandidateId", "RecruiterId", "RequiredEducation", "Status", "Title" },
                values: new object[,]
                {
                    { 1, null, 2, 2, "Opened", "Software Developer II" },
                    { 2, null, 2, 3, "Opened", "Senior Software Developer" },
                    { 3, null, 1, 1, "Opened", "Software Developer - Entry" },
                    { 4, null, 1, 2, "Opened", "Software Developer I" },
                    { 5, null, 2, 1, "Opened", "Software Developer - Entry" },
                    { 6, null, 2, 2, "Opened", "Software Developer I" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobCandidateMatches_CandidateId",
                table: "JobCandidateMatches",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_HiredCandidateId",
                table: "Jobs",
                column: "HiredCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_RecruiterId",
                table: "Jobs",
                column: "RecruiterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobCandidateMatches");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Recruiters");
        }
    }
}
