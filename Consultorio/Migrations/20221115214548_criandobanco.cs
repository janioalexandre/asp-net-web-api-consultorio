using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Consultorio.Migrations
{
    public partial class criandobanco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_especialidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: false),
                    ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_especialidade", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_paciente",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(50)", nullable: true),
                    celular = table.Column<string>(type: "varchar(100)", nullable: false),
                    cpf = table.Column<string>(type: "varchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_paciente", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_profissional",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_profissional", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_consulta",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    data_horario = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    preco = table.Column<decimal>(type: "numeric(7,2)", precision: 7, scale: 2, nullable: false),
                    id_paciente = table.Column<int>(type: "integer", nullable: false),
                    id_especialidade = table.Column<int>(type: "integer", nullable: false),
                    id_profissional = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_consulta", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_consulta_tb_especialidade_id_especialidade",
                        column: x => x.id_especialidade,
                        principalTable: "tb_especialidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_consulta_tb_paciente_id_paciente",
                        column: x => x.id_paciente,
                        principalTable: "tb_paciente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_consulta_tb_profissional_id_profissional",
                        column: x => x.id_profissional,
                        principalTable: "tb_profissional",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_profissional_especialidade",
                columns: table => new
                {
                    id_profissional = table.Column<int>(type: "integer", nullable: false),
                    id_especialidade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_profissional_especialidade", x => new { x.id_especialidade, x.id_profissional });
                    table.ForeignKey(
                        name: "FK_tb_profissional_especialidade_tb_especialidade_id_especiali~",
                        column: x => x.id_especialidade,
                        principalTable: "tb_especialidade",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_profissional_especialidade_tb_profissional_id_profission~",
                        column: x => x.id_profissional,
                        principalTable: "tb_profissional",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_consulta_id_especialidade",
                table: "tb_consulta",
                column: "id_especialidade");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consulta_id_paciente",
                table: "tb_consulta",
                column: "id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consulta_id_profissional",
                table: "tb_consulta",
                column: "id_profissional");

            migrationBuilder.CreateIndex(
                name: "IX_tb_profissional_especialidade_id_profissional",
                table: "tb_profissional_especialidade",
                column: "id_profissional");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_consulta");

            migrationBuilder.DropTable(
                name: "tb_profissional_especialidade");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "tb_paciente");

            migrationBuilder.DropTable(
                name: "tb_especialidade");

            migrationBuilder.DropTable(
                name: "tb_profissional");
        }
    }
}
