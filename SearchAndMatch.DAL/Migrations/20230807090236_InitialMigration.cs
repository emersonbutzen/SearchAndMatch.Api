using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SearchAndMatch.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "wmda");

            migrationBuilder.CreateTable(
                name: "Engines",
                schema: "wmda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Endpoint = table.Column<string>(type: "text", nullable: false),
                    Schema = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "wmda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<string>(type: "text", nullable: false),
                    DiseaseType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });


            migrationBuilder.Sql(
                @"INSERT INTO wmda.""Engines""
                (""Id"", ""Endpoint"", ""Schema"")
                VALUES
				(1, 'https://eo3l5eklg34uog1.m.pipedream.net', '{""patient"": {""forename"": """",""surname"": """",""dateOfBirth"":"""",""diseaseType"": """"}}'),
                (2, 'https://eo8xmfbl1hdsedj.m.pipedream.net', '{""patient"": {""firstName"": """",""lastName"": """",""dateOfBirth"": """",""diseaseType"": """"}}');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Engines",
                schema: "wmda");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "wmda");
        }
    }
}
